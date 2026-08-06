using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.NutritionistRequest
{
    public class UpdateNutritionistRequest
    {
        public Guid Id {get; set;}
        public Guid UserId {get; set;}
        public string Crn { get; set; }          
        public string Especialidade { get; set; } 
        public Guid AddressId { get; set; }
        public bool IsActive {get; set;}

    }
}