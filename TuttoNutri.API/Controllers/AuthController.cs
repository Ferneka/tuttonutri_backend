using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TuttoNutri.API.Application.Models.Request.Register;
using TuttoNutri.API.Application.Services.AuthService;

namespace TuttoNutri.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : DefaultController
    {
         private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            var result = await _service.RegisterAsync(request);

            if (result is null) return BadRequest();

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(CreateLoginRequest request)
        {
            var result = await _service.LoginAsync(request);

            if (result is null) return BadRequest();

            return Ok(result);
        }
    }
}