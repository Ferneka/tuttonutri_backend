using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.Request.MedicalRecordRequest;
using TuttoNutri.API.Application.Services.MedicalRecordService;
using TuttoNutri.API.Filters;

namespace TuttoNutri.API.Controllers
{
    [Authorize(Roles = "Nutritionist")]
    [RequireActiveSubscription]
    public class MedicalRecordController : DefaultController
    {
        private readonly IMedicalRecordService _service;

        public MedicalRecordController(IMedicalRecordService service)
        {
            _service = service;
        }

        private Guid GetNutritionistId()
        {
            var claim = User.FindFirst("NutritionistId")?.Value;
            return Guid.Parse(claim);
        }

        // Equivale a abrir o modal "Cadastrar Prontuário" e escolher paciente/data.
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateMedicalRecordRequest request)
        {
            try
            {
                var result = await _service.Create(request, GetNutritionistId());
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Equivale ao botão "Salvar" na tela de Antropometria — salva o objeto inteiro.
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateMedicalRecordRequest request)
        {
            try
            {
                var result = await _service.Update(request, GetNutritionistId());
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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

        // Equivale ao "Histórico de Prontuários: <paciente>" na tela principal.
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByPatient(Guid patientId)
        {
            try
            {
                var result = await _service.GetByPatient(patientId, GetNutritionistId());
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
    }
}