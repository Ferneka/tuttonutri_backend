using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    
    public interface IMedicalRecordRepository : IRepository<MedicalRecord, Guid>
    {
        Task<List<MedicalRecord>> GetByPatientId(Guid patientId);
        void Update(MedicalRecord medicalRecord);
    }
}