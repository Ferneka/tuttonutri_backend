using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class NutritionistDTO
    {
        public Guid Id {get; set;}
        public UserSummaryDTO User {get; set;}
        public string Crn { get; set; }          
        public bool IsActive {get; set;}
    }
}