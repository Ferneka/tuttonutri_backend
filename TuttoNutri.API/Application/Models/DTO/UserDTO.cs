using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role {get; set;}
        public string Email { get; set; }
        public string Token { get; set; } 
        public Guid? NutritionistId { get; set; }
    }
}