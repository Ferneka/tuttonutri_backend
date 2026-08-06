using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class NutritionistDTOExtension
    {
        public static NutritionistDTO ToDTO(this Nutritionist nutritionist)
        {
            if(nutritionist is null) return null;

            return new NutritionistDTO
            {
                Id = nutritionist.Id,  
                User = nutritionist.User.ToSummaryDTO(),
                Crn = nutritionist.Crn,
                IsActive = nutritionist.IsActive,
            };
        }
    }
}