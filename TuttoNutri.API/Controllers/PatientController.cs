using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.Request.PatientRequest;
using TuttoNutri.API.Application.Services.PatientService;
using TuttoNutri.API.Filters;

namespace TuttoNutri.API.Controllers
{
    [Authorize(Roles = "Nutritionist")]
    [RequireActiveSubscription]
    public class PatientController : DefaultController
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        private Guid GetNutritionistId()
        {
            var claim = User.FindFirst("NutritionistId")?.Value;
            return Guid.Parse(claim);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreatePatientRequest request)
        {
            var result = await _service.Create(request, GetNutritionistId());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id, GetNutritionistId());

            if (result is null) return NotFound();

            return Ok(result);
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll(GetNutritionistId());

            return Ok(data);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdatePatientRequest request)
        {
            var result = await _service.Update(request, GetNutritionistId());

            if (result is false) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _service.Deactivate(id, GetNutritionistId());

            if (result is false) return BadRequest();

            return Ok(result);
        }
        
        [HttpPatch("{id}/activate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Activate(Guid id)
        {
            var result = await _service.Activate(id, GetNutritionistId());

            if (result is false) return BadRequest();

            return Ok(result);
        }
    }
}