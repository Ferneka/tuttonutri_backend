using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.Register;
using TuttoNutri.API.Application.Services.AuthService;
using TuttoNutri.API.Application.Exceptions;
using System.Security.Claims;

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
            try
            {
                var result = await _service.LoginAsync(request);

                if (result is null) return BadRequest();

                return Ok(result);
            }
            catch (EmailNotConfirmedException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, new
                {
                    code = EmailNotConfirmedException.CodeValue,
                    email = request.Email
                });
            }
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO dto)
        {
            await _service.ForgotPasswordAsync(dto);
            return Ok();
        }

        [HttpPost("validate-code")]
        public async Task<IActionResult> ValidateCode(ValidateCodeDTO dto)
        {
            var isValid = await _service.ValidateCodeAsync(dto);
            if (!isValid) return BadRequest("Código inválido ou expirado.");
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var result = await _service.ResetPasswordAsync(dto);
            if (!result.Success) return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(ValidateCodeDTO dto)
        {
            var result = await _service.VerifyEmailAsync(dto);
            if (result is null) return BadRequest(new { message = "Código inválido ou expirado." });
            return Ok(result);
        }

        [HttpPost("resend-verification-code")]
        public async Task<IActionResult> ResendVerificationCode(ResendCodeDTO dto)
        {
            await _service.ResendVerificationCodeAsync(dto.Email);
            return Ok();
        }
       
    }
}