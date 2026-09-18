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
                Fiber = foodPlan.Fiber, // NOVO
                Observations = foodPlan.Observations,
                InitDate = foodPlan.InitDate,
                EndDate = foodPlan.EndDate,
                Nutritionist = foodPlan.Nutritionist?.ToDTO(),
                PatientId = foodPlan.PatientId, 
                IsActive = foodPlan.IsActive,
                Meals = foodPlan.Meals.Select(m => new MealDTO 
                {
                    Id = m.Id,
                    Name = m.Name,
                    Time = m.Time,
                    Items = m.Items.Select(i => new MealFoodItemDTO
                    {
                        Id = i.Id,
                        TacoId = i.TacoId,
                        Description = i.Description,
                        Grams = i.Grams,
                        KcalPer100g = i.KcalPer100g,
                        ProteinPer100g = i.ProteinPer100g,
                        FatPer100g = i.FatPer100g,
                        CarbohydratePer100g = i.CarbohydratePer100g,
                        FiberPer100g = i.FiberPer100g,
                    }).ToList()
                }).ToList()
            };
        }
    }
}