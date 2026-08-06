using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class FoodPlan : BaseModel, IAggregateRoot
    {
        public string Name { get; private set; }          
        public double Calories { get; private set; } 
        public double Protein { get; private set; }     
        public double Carbohydrate { get; private set; }  
        public double Fat { get; private set; }      
        public string Observations  { get; private set; }
        public DateTime InitDate { get; private set; }
        public DateTime? EndDate { get; private set; }    
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }
        public FoodPlan(string name, double calories, double protein, double carbohydrate, double fat, 
        string observations, DateTime initDate, DateTime? endDate, Guid patientId, Guid nutritionistId)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Observations = observations;
            InitDate = initDate;
            EndDate = endDate;
            PatientId = patientId;
            NutritionistId = nutritionistId;
        }

        public void Update(string name, double calories, double protein, double carbohydrate, double fat, 
        string observations, DateTime initDate, DateTime endDate, Guid patientId, Guid nutritionistId, bool isActive)
        {
             Name = name;
            Calories = calories;
            Protein = protein;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Observations = observations;
            InitDate = initDate;
            EndDate = endDate;
            PatientId = patientId;
            NutritionistId = nutritionistId;
            IsActive = isActive;
        }
    }
}