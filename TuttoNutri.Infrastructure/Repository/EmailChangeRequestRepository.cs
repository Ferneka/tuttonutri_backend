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
    public class EmailChangeRequestRepository : IEmailChangeRequestRepository
    {
        private readonly ApplicationDataContext _context;

        public EmailChangeRequestRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmailChangeRequest request)
        {
            await _context.Set<EmailChangeRequest>().AddAsync(request);
        }

        public async Task<EmailChangeRequest> GetValidAsync(string userId, string code)
        {
            return await _context.Set<EmailChangeRequest>()
                .Where(r => r.UserId == userId && r.Code == code && !r.Used)
                .OrderByDescending(r => r.ExpiresAt)
                .FirstOrDefaultAsync();
        }
    }
}