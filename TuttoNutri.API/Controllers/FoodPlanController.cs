using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.Request.FoodPlanRequest;
using TuttoNutri.API.Application.Services.FoodPlanService;

namespace TuttoNutri.API.Controllers
{
    public class FoodPlanController : DefaultController 
    {
        private readonly IFoodPlanService _service;

        public FoodPlanController(IFoodPlanService service)
        {
            _service = service;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateFoodPlanRequest request)
        {
            var result = await _service.Add(request);

            if (result is false) return BadRequest();

            return Ok(result);
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

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateFoodPlanRequest request)
        {
            var result = await _service.Update(request);

            if (result is false) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _service.Deactivate(id);

            if (result is false) return BadRequest();

            return Ok(result);
        }
    }
}
