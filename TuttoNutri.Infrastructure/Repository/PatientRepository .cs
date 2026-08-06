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
            //.Include(a => a.Address)
            .Where(a => a.IsActive == true)
            .ToListAsync();
        }

        public async Task<Patient> GetById(Guid id)
        {
             return await _entity
            //.Include(a => a.Address)
            .Where(a => a.IsActive == true)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Patient entity)
        {
            _entity.Update(entity);
        }
    }
}