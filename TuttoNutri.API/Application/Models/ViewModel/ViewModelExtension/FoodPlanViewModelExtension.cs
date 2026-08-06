using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;

namespace TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension
{
    public static class FoodPlanViewModelExtension
    {
        public static FoodPlanViewModel ToViewModel(this FoodPlanDTO DTO)
        {
            if(DTO is null) return null;

            return new FoodPlanViewModel
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Calories = DTO.Calories,
                Protein = DTO.Protein,
                Carbohydrate = DTO.Carbohydrate,
                Fat = DTO.Fat,
                Observations = DTO.Observations,
                InitDate = DTO.InitDate,
                EndDate = DTO.EndDate,
                Nutritionist = DTO.Nutritionist.ToViewModel(),
                //Patient = DTO.Patient.ToViewModel(),
                IsActive = DTO.IsActive
            };
        }
    }
}