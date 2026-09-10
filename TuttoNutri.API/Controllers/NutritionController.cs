using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Filters;
using TuttoNutri.Infrastructure.Nutrition;

namespace TuttoNutri.API.Controllers
{
    [Authorize(Roles = "Nutritionist")]
    [RequireActiveSubscription]
    public class NutritionController : DefaultController
    {
        private readonly TacoDataProvider _tacoData;
 
    public NutritionController(TacoDataProvider tacoData)
    {
        _tacoData = tacoData;
    }
 
    
    [HttpGet("foods")]
    public ActionResult<IEnumerable<FoodSearchResultDTO>> Search([FromQuery] string query, [FromQuery] int take = 8)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Trim().Length < 2)
            return Ok(Array.Empty<FoodSearchResultDTO>());
 
        var termo = TextNormalizer.Normalize(query);
 
        var resultados = _tacoData.Todos
            .Where(item => TextNormalizer.Normalize(item.Descricao).Contains(termo))
            .OrderBy(item => item.Descricao.Length)
            .Take(Math.Clamp(take, 1, 20))
            .Select(item => new FoodSearchResultDTO
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Categoria = item.Categoria,
                Kcal = item.Kcal ?? 0,
                ProteinaG = item.ProteinaG ?? 0,
                GorduraG = item.GorduraG ?? 0,
                CarboidratoG = item.CarboidratoG ?? 0,
                FibraG = item.FibraG ?? 0
            });
 
        return Ok(resultados);
    }
    }
}