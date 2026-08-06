using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class UserDTOExtension
    {
        public static UserDTO ToDTO(this User user)
        {
            if(user is null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
                
            };

        }
    }
}