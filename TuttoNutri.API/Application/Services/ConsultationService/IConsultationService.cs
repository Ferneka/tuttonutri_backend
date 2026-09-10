using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.ConsultationRequest;

namespace TuttoNutri.API.Application.Services.ConsultationService
{
    public interface IConsultationService
    {
        Task<ConsultationDTO> Create(CreateConsultationRequest request, Guid nutritionistId);
        Task<List<ConsultationDTO>> GetAll(Guid nutritionistId);
        Task<ConsultationDTO> GetById(Guid id, Guid nutritionistId);
        Task<bool> Update(UpdateConsultationRequest request, Guid nutritionistId);
        Task<bool> Deactivate(Guid id, Guid nutritionistId);
    }

    
}