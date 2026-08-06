using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;

namespace TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension
{
    public static class PatientViewModelExtension
    {
        public static PatientViewModel ToViewModel(this PatientDTO DTO)
        {
            if (DTO is null) return null;

            return new PatientViewModel
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Email = DTO.Email,
                Phone = DTO.Phone,
                Gender = DTO.Gender,
                BirthOfDate = DTO.BirthOfDate,
                Weight = DTO.Weight,
                Height = DTO.Height,
                Objective = DTO.Objective,
                IsActive = DTO.IsActive,
                Address = DTO.Address.ToViewModel()
            };
        }
    }
}