using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(User user, string role, Guid? nutritionistId = null);
    }
}