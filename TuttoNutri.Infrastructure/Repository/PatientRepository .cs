using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;

namespace TuttoNutri.Infrastructure.Repository
{
    public class PatientRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : IPatientRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<Patient> _entity = context.Set<Patient>();
        public IUnitOfWork UnitOfWork => unitOfWork;

        public async Task<bool> Activate(Guid id)
        {
            var patient = await _entity.FindAsync(id);
            if (patient == null) return false;

            patient.IsActive = true;
            return true;
        }

        public void Add(Patient entity)
        {
            _entity.Add(entity);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var patient = await _entity.FindAsync(id);
        
            if (patient is null) return;

            patient.Deactivate(); 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _entity
            .ToListAsync();
        }

        public async Task<Patient> GetById(Guid id)
        {
             return await _entity
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Patient patient)
        {
            _entity.Update(patient);
        }
    }
}