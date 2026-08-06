using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;
using TuttoNutri.API.Application.Models.Request.Register;
using TuttoNutri.API.Application.Services.TokenService;

namespace TuttoNutri.API.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly INutritionistRepository _nutritionistRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, IAddressRepository addressRepository, INutritionistRepository nutritionistRepository, 
         ITokenService tokenService)
        {
            _authRepository = authRepository;
            _addressRepository = addressRepository;
            _nutritionistRepository = nutritionistRepository;
            _tokenService = tokenService;
        }
        public async Task<UserDTO> LoginAsync(CreateLoginRequest request)
        {
            var user = await _authRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new Exception($"Usuário não encontrado: {request.Email}");

            var senhaValida = await _authRepository.CheckPasswordAsync(user, request.Password);
            if (!senhaValida)
                throw new Exception($"Senha inválida para o usuário: {request.Email}");

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
                Token = token
            };
        }

        public async Task<UserDTO> RegisterAsync(CreateUserRequest request)
        {
            var user = new User(
                name: request.Name,
                email: request.Email
            );

            await _authRepository.AddUserAsync(user, request.Password);

            await _addressRepository.UnitOfWork.SaveEntitiesAsync();

            await _authRepository.AddToRoleAsync(user, "Nutritionist");

            var nutritionist = new Nutritionist(
                userId: user.Id,
                crn: request.Crn
            );
            _nutritionistRepository.Add(nutritionist);
            await _nutritionistRepository.UnitOfWork.SaveEntitiesAsync();

            var token = _tokenService.GenerateToken(user, "Nutritionist", nutritionist.Id);

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = "Nutritionist",
                Token = token
            };
        }
    }
}