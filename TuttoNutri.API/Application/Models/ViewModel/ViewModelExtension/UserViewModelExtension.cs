using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;

namespace TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension
{
    public static class UserViewModelExtension
    {
        public static UserViewModel ToViewModel(this UserDTO DTO)
        {
            return new UserViewModel
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Gender  = DTO.Gender,
                Email = DTO.Email,
                BirthOfDate = DTO.BirthOfDate
            };
        }
    }
}