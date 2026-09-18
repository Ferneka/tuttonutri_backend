using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.Request.NutritionistRequest;
using TuttoNutri.API.Application.Models.Request.Register;
using TuttoNutri.API.Application.Services.AuthService;
using TuttoNutri.API.Application.Services.NutritionistService;
using TuttoNutri.API.Filters;

namespace TuttoNutri.API.Controllers
{
    [Authorize(Roles = "Nutritionist")]
    public class NutritionistController : DefaultController
    {
        private readonly INutritionistService _service;
        private readonly IAuthService _authService;

        public NutritionistController(INutritionistService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();

            if (data.Any() is false) return NoContent();

            return Ok(data);
        }
        [RequireActiveSubscription]
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateNutritionistRequest request)
        {
            var result = await _service.Update(request);

            if (result is false) return BadRequest();

            return Ok(result);
        }

        [HttpDelete()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _service.Deactivate(id);

            if (result is false) return BadRequest();

            return Ok(result);
        }
        [HttpPost("email/request-change")]
        public async Task<IActionResult> RequestEmailChange([FromBody] RequestEmailChangeRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.RequestEmailChangeAsync(userId, request.NewEmail);
            return Ok(result);
        }

        [HttpPost("email/confirm-change")]
        public async Task<IActionResult> ConfirmEmailChange([FromBody] ConfirmEmailChangeRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.ConfirmEmailChangeAsync(userId, request.Code);
            if (!result) return BadRequest("Código inválido ou expirado.");
            return Ok(result);
        }
    }
}