using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
        Task<List<Address>> GetAll();
        Task DeactivateAsync(Guid id);
    }
}