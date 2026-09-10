using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class PatientDTO
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Phone {get; set;}
        public string Gender {get; set;}
        public DateTime BirthOfDate {get; set;}
        public double Height {get; set;}
        public bool IsActive {get; set;}

    }
}