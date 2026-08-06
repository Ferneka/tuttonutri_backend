using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.ViewModel
{
    public class NutritionistViewModel
    {
        public Guid Id {get; set;}
        public UserViewModel User {get; set;}
        public string Crn { get; set; }          
        public bool IsActive { get; set; }


    }
}