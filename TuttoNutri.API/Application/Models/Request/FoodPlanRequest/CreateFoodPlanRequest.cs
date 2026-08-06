using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.FoodPlanRequest
{
    public class CreateFoodPlanRequest
    {
        public string Name {get; set;}
        public double Calories {get; set;}
        public double Protein {get; set;}
        public double Carbohydrate  {get; set;}
        public double Fat {get; set;}
        public string Observations {get; set;}
        public DateTime InitDate { get; set;}
        public DateTime? EndDate {get; set;}
        public Guid NutritionistId {get; set;}
        public Guid PatientId {get; set;}
        public bool IsActive {get; set;}
    }
}