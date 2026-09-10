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
    public class ConsultationRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : IConsultationRepository
    {
        private readonly ApplicationDataContext _context = context;
        public IUnitOfWork UnitOfWork => unitOfWork;

        public void Add(Consultation consultation)
        {
             _context.Consultation.Add(consultation);
        }

        public async Task<List<Consultation>> GetAll()
        {
             return await _context.Consultation
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Consultation> GetById(Guid id)
        {
             return await _context.Consultation.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Consultation consultation)
        {
            throw new NotImplementedException();
        }
    }
}
