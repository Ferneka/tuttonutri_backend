using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class ConsultationDTOExtension
    {
        public static ConsultationDTO ToDTO(this Consultation consultation)
        {
            return new ConsultationDTO
            {
                Id = consultation.Id,
                Date = consultation.Date,
                Status = consultation.Status,
                PatientId = consultation.PatientId,
                NutritionistId = consultation.NutritionistId,
                IsActive = consultation.IsActive
            };
        }   
    }
}