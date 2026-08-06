using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.PatientRequest;

namespace TuttoNutri.API.Application.Services.PatientService
{
    public interface IPatientService
    {
        Task<PatientDTO> Create(CreatePatientRequest request, Guid nutritionistId);
        Task<PatientDTO> GetById(Guid id, Guid nutritionistId);
        Task<List<PatientDTO>> GetAll(Guid nutritionistId);
        Task<bool> Update(UpdatePatientRequest request, Guid nutritionistId);
        Task<bool> Deactivate(Guid id, Guid nutritionistId);
    }
}