using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.ViewModel;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class UserSummaryDTOExtension
    {
        public static UserSummaryDTO ToSummaryDTO(this User user)
        {
            if(user is null) return null;

            return new UserSummaryDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
        public static UserViewModel ToViewModel(this UserSummaryDTO dto)
        {
            if (dto is null) return null;

            return new UserViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email
            };
        }
    }
}