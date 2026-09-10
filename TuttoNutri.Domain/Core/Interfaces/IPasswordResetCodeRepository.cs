using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IPasswordResetCodeRepository
    {
        Task AddAsync(PasswordResetCode code);
        Task<PasswordResetCode> GetValidAsync(string email, string code);
    }
}