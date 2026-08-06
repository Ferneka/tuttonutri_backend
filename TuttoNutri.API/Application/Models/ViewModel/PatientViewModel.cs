using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.ViewModel
{
    public class PatientViewModel
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
        public string Gender {get; set;}
        public DateTime BirthOfDate {get; set;}
        public double Weight { get; set; }
        public double Height  { get; set; }
        public string Objective { get; set; }  
        public AddressViewModel Address { get; set; }
        public bool IsActive { get; set; }

    }
}