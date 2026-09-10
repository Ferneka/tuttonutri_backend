using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.API.Filters
{
    public class RequireActiveSubscriptionAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idClaim = context.HttpContext.User.FindFirst("NutritionistId")?.Value;
            if (idClaim == null || !Guid.TryParse(idClaim, out var id))
            {
                await next();
                return;
            }

            var repo = context.HttpContext.RequestServices.GetRequiredService<INutritionistRepository>();
            var nutritionist = await repo.GetById(id);

            if (nutritionist == null || nutritionist.DataExpiracao < DateTime.UtcNow)
            {
                context.Result = new ObjectResult(new { message = "Assinatura expirada ou período de teste encerrado" })
                {
                    StatusCode = 402 
                };
                return;
            }

            await next();
        }
    }
}
