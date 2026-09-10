using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IConsultationRepository
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(Consultation consultation);
        Task<List<Consultation>> GetAll();
        Task<Consultation> GetById(Guid id);
        void Update(Consultation consultation);
        
    }
}