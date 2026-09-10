using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace TuttoNutri.Infrastructure.Repository
{
    public class EmailVerificationCodeRepository : IEmailVerificationCodeRepository
    {
        private readonly ApplicationDataContext _context;

        public EmailVerificationCodeRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmailVerificationCode code)
        {
            await _context.EmailVerificationCodes.AddAsync(code);
        }

        public async Task<EmailVerificationCode> GetValidAsync(string email, string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            return await _context.EmailVerificationCodes
                .Where(p => p.UserId == user.Id && p.Code == code)
                .OrderByDescending(p => p.ExpiresAt)
                .FirstOrDefaultAsync();
        }
    }
}