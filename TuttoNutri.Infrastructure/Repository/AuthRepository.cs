using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;

namespace TuttoNutri.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDataContext _context;
        public IUnitOfWork UnitOfWork { get; }

        public AuthRepository(UserManager<User> userManager, ApplicationDataContext context, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _context = context;
            UnitOfWork = unitOfWork;
        }
        public async Task AddToRoleAsync(User user, string role)
        => await _userManager.AddToRoleAsync(user, role);

        public async Task AddUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
         => await _userManager.CheckPasswordAsync(user, password);

        public async Task<User> GetByEmailAsync(string email)
         => await _userManager.FindByEmailAsync(email);

        public async Task<IList<string>> GetRolesAsync(User user)
         => await _userManager.GetRolesAsync(user);
    }
} 