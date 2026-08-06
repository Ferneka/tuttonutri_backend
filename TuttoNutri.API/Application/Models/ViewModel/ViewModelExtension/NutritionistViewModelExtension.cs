using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;

namespace TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension
{
    public static class NutritionistViewModelExtension
    {
        public static NutritionistViewModel ToViewModel(this NutritionistDTO DTO)
        {

            if(DTO is null) return null;

            return new NutritionistViewModel
            {
                Id = DTO.Id,
                //UserId = DTO.UserId,
                User = DTO.User.ToViewModel(),
                Crn = DTO.Crn,
                IsActive = DTO.IsActive,
            };
        }
    }
}