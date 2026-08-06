using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.PatientRequest
{
    public class CreatePatientRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthOfDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Objective { get; set; }
        public Guid AddressId { get; set; }
    }
}