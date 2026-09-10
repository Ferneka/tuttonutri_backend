using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class PatientDTOExtension
    {
        public static PatientDTO ToDTO(this Patient patient)
        {
            if(patient is null) return null;

            return new PatientDTO
            {
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                Gender = patient.Gender,
                BirthOfDate = patient.BirthOfDate,   
                Height = patient.Height,             
                IsActive = patient.IsActive,
            };
        }
    }
}