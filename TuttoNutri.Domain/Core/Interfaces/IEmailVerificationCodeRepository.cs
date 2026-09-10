using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IEmailVerificationCodeRepository
    {
        Task AddAsync(EmailVerificationCode code);
        Task<EmailVerificationCode> GetValidAsync(string email, string code);
    }
}