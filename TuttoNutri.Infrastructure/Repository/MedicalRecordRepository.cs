using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;

namespace TuttoNutri.Infrastructure.Data.Repositories
{
    
    public class MedicalRecordRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : IMedicalRecordRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<MedicalRecord> _entity = context.Set<MedicalRecord>();
        public IUnitOfWork UnitOfWork => unitOfWork;


        public void Add(MedicalRecord entity) => _context.Add(entity);

        public async Task<MedicalRecord> GetById(Guid id) =>
            await _entity.FirstOrDefaultAsync(mr => mr.Id == id);

        public async Task<List<MedicalRecord>> GetAll() =>
            await _entity.ToListAsync();

        public async Task<List<MedicalRecord>> GetByPatientId(Guid patientId) =>
            await _entity
                .Where(mr => mr.PatientId == patientId && mr.IsActive)
                .OrderByDescending(mr => mr.EvaluationDate)
                .ToListAsync();

        public void Update(MedicalRecord medicalRecord) => _entity.Update(medicalRecord);
        
    }
}