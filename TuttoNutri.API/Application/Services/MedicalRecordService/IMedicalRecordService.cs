using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.MedicalRecordRequest;

namespace TuttoNutri.API.Application.Services.MedicalRecordService
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecordDTO> Create(CreateMedicalRecordRequest request, Guid nutritionistId);
        Task<MedicalRecordDTO> Update(UpdateMedicalRecordRequest request, Guid nutritionistId);
        Task<MedicalRecordDTO> GetById(Guid id, Guid nutritionistId);
        Task<List<MedicalRecordDTO>> GetByPatient(Guid patientId, Guid nutritionistId);
        Task<bool> Deactivate(Guid id, Guid nutritionistId);
    }
}