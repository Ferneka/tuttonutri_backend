using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.Request.AddressRequest;
using TuttoNutri.API.Application.Models.ViewModel;
using TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension;
using TuttoNutri.API.Application.Services.AddressService;

namespace TuttoNutri.API.Controllers
{
    [Authorize]
    public class AddressController : DefaultController
    {
        private readonly IAddressService _service;
        public AddressController(IAddressService service)
        {
            _service = service;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateAddressRequest request)
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
        [ProducesResponseType(typeof(List<AddressViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();

            if (data.Any() is false) return NoContent();

            var result = data.Select(x => x.ToViewModel());

            return Ok(result);
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateAddressRequest request)
        {
            var result = await _service.Update(request);

            if (result is false) return BadRequest();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);

            if (result is false) return BadRequest();

            return Ok(result);
        }
    }
}