using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IPatientRepository : IRepository<Patient, Guid>
    {
        Task<List<Patient>> GetAll();
        Task DeactivateAsync(Guid id);
        Task<bool> Activate(Guid id);
        void Update(Patient patient);

    }
}