using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.Register;

namespace TuttoNutri.API.Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(CreateUserRequest request);
        Task<UserDTO> LoginAsync(CreateLoginRequest request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDTO dto);
        Task<bool> ValidateCodeAsync(ValidateCodeDTO dto);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordDTO dto);
        Task<UserDTO> VerifyEmailAsync(ValidateCodeDTO dto);
        Task<bool> ResendVerificationCodeAsync(string email);
        Task<bool> RequestEmailChangeAsync(string userId, string newEmail);
        Task<bool> ConfirmEmailChangeAsync(string userId, string code);

    }
}