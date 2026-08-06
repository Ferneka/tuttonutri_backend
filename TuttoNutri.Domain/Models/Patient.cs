using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class Patient : BaseModel, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Gender { get; private set; }
        public DateTime BirthOfDate { get; private set; }

        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }

        public ICollection<Consultation> Consultation { get; set; }

        public Patient(string name, string phone, string gender, DateTime birthOfDate, Guid nutritionistId)
        {
            Name = name;
            Phone = phone;
            Gender = gender;
            BirthOfDate = birthOfDate;
            NutritionistId = nutritionistId;
        }

        public void Update(string name, string phone, string gender, DateTime birthOfDate)
        {
            Name = name;
            Phone = phone;
            Gender = gender;
            BirthOfDate = birthOfDate;
        }
    }
}