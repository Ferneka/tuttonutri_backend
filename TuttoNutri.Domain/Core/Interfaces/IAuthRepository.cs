using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user, string password);
        Task AddToRoleAsync(User user, string role);
        Task<IList<string>> GetRolesAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}