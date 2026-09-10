using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;

namespace TuttoNutri.Infrastructure.Repository
{
    public class PasswordResetCodeRepository : IPasswordResetCodeRepository
    {
        private readonly ApplicationDataContext _context;

        public PasswordResetCodeRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PasswordResetCode code)
        {
            await _context.PasswordResetCodes.AddAsync(code);
        }

        public async Task<PasswordResetCode> GetValidAsync(string email, string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            return await _context.PasswordResetCodes
                .Where(p => p.UserId == user.Id && p.Code == code)
                .OrderByDescending(p => p.ExpiresAt)
                .FirstOrDefaultAsync();
        }
    }
}