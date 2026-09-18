using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IEmailChangeRequestRepository
    {
        Task AddAsync(EmailChangeRequest request);
        Task<EmailChangeRequest> GetValidAsync(string userId, string code);
    }
}