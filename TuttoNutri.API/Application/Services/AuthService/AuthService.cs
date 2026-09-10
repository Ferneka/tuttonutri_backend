using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;
using TuttoNutri.API.Application.Models.Request.Register;
using TuttoNutri.API.Application.Services.TokenService;
using TuttoNutri.API.Application.Services.EmailService;
using TuttoNutri.API.Application.Exceptions;

namespace TuttoNutri.API.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly INutritionistRepository _nutritionistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordResetCodeRepository _resetCodeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IEmailVerificationCodeRepository _verificationCodeRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAuthRepository authRepository, IAddressRepository addressRepository, INutritionistRepository nutritionistRepository,
         ITokenService tokenService, IPasswordResetCodeRepository resetCodeRepository, UserManager<User> userManager, IEmailService emailService,
         IUnitOfWork unitOfWork, IEmailVerificationCodeRepository verificationCodeRepository, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _addressRepository = addressRepository;
            _nutritionistRepository = nutritionistRepository;
            _tokenService = tokenService;
            _userManager = userManager;
            _resetCodeRepository = resetCodeRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _verificationCodeRepository = verificationCodeRepository;
            _logger = logger;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return true;

            var code = new Random().Next(100000, 999999).ToString();

            await _resetCodeRepository.AddAsync(new PasswordResetCode
            {
                UserId = user.Id,
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                Used = false
            });

            await _unitOfWork.SaveChangesAsync();

            try
            {
                await _emailService.SendAsync(user.Email, "Código de verificação",
                    $"Seu código é: {code}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao enviar e-mail de recuperação de senha para {Email}", user.Email);
                // não relança: o código já está salvo no banco, o usuário pode pedir reenvio
            }

            return true;
        }

        public async Task<UserDTO> LoginAsync(CreateLoginRequest request)
        {
            var user = await _authRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            var senhaValida = await _authRepository.CheckPasswordAsync(user, request.Password);
            if (!senhaValida)
                return null;

            var roles = await _authRepository.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var nutritionist = await _nutritionistRepository.GetByUserId(user.Id);

            var token = _tokenService.GenerateToken(user, role, nutritionist?.Id);

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = role,
                NutritionistId = nutritionist?.Id,
                Token = token
            };
        }

        public async Task<UserDTO> RegisterAsync(CreateUserRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new BusinessException("As senhas não coincidem.");

            var existingUser = await _authRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new BusinessException("Já existe um usuário cadastrado com esse email.");

            var existingNutritionist = await _nutritionistRepository.GetByCrn(request.Crn);
            if (existingNutritionist != null)
                throw new BusinessException("Já existe um nutricionista cadastrado com esse CRN.");

            var user = new User(
                name: request.Name,
                email: request.Email
            );

            await _authRepository.AddUserAsync(user, request.Password);

            await _authRepository.AddToRoleAsync(user, "Nutritionist");

            var nutritionist = new Nutritionist(
                userId: user.Id,
                crn: request.Crn
            );
            _nutritionistRepository.Add(nutritionist);
            await _nutritionistRepository.UnitOfWork.SaveEntitiesAsync();

            var verificationCode = new Random().Next(100000, 999999).ToString();
            await _verificationCodeRepository.AddAsync(new EmailVerificationCode
            {
                UserId = user.Id,
                Code = verificationCode,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                Used = false
            });
            await _unitOfWork.SaveChangesAsync();

            // Gera o token ANTES de tentar enviar o e-mail, para que o cadastro
            // seja concluído com sucesso mesmo que o envio de e-mail falhe.
            var token = _tokenService.GenerateToken(user, "Nutritionist", nutritionist.Id);

            try
            {
                await _emailService.SendAsync(user.Email, "Verifique seu e-mail",
                    $"Seu código de verificação é: {verificationCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao enviar e-mail de verificação para {Email}", user.Email);
                // não relança: o usuário e o código já foram salvos no banco,
                // o front pode oferecer "reenviar código" depois.
            }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = "Nutritionist",
                NutritionistId = nutritionist.Id,
                Token = token
            };
        }

        public async Task<bool> ResendVerificationCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return true; // não revela se o email existe

            var code = new Random().Next(100000, 999999).ToString();

            await _verificationCodeRepository.AddAsync(new EmailVerificationCode
            {
                UserId = user.Id,
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                Used = false
            });

            await _unitOfWork.SaveChangesAsync();

            try
            {
                await _emailService.SendAsync(user.Email, "Verifique seu e-mail",
                    $"Seu código de verificação é: {code}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao reenviar e-mail de verificação para {Email}", user.Email);
            }

            return true;
        }

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            var record = await _resetCodeRepository.GetValidAsync(dto.Email, dto.Code);

            if (record == null || record.ExpiresAt < DateTime.UtcNow || record.Used)
                return ResetPasswordResult.Fail("Código inválido ou expirado.");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return ResetPasswordResult.Fail("Usuário não encontrado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var identityResult = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!identityResult.Succeeded)
                return ResetPasswordResult.Fail(identityResult.Errors.Select(e => e.Description));

            record.Used = true;
            await _unitOfWork.SaveChangesAsync();

            return ResetPasswordResult.Ok();
        }

        public async Task<bool> ValidateCodeAsync(ValidateCodeDTO dto)
        {
            var record = await _resetCodeRepository.GetValidAsync(dto.Email, dto.Code);

            if (record == null || record.ExpiresAt < DateTime.UtcNow || record.Used)
                return false;

            return true;
        }

        public async Task<bool> VerifyEmailAsync(ValidateCodeDTO dto)
        {
            var record = await _verificationCodeRepository.GetValidAsync(dto.Email, dto.Code);

            if (record == null || record.ExpiresAt < DateTime.UtcNow || record.Used)
                return false;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            record.Used = true;
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}