using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class FoodPlanDTOExtension
    {
        public static FoodPlanDTO ToDTO(this FoodPlan foodPlan)
        {
            if( foodPlan is null ) return null;

            return new FoodPlanDTO
            {
                Id = foodPlan.Id,
                Name = foodPlan.Name,
                Calories = foodPlan.Calories,
                Protein = foodPlan.Protein,
                Carbohydrate = foodPlan.Carbohydrate,
                Fat = foodPlan.Fat,
                Observations = foodPlan.Observations,
                InitDate = foodPlan.InitDate,
                EndDate = foodPlan.EndDate,
                Nutritionist = foodPlan.Nutritionist.ToDTO(),
                //Patient = foodPlan.Patient.ToDTO()
            };
        }
    }
}