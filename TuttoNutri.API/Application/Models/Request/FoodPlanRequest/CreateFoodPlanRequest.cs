using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.Request.MealRequest; // ajuste se o namespace real for outro

namespace TuttoNutri.API.Application.Models.Request.FoodPlanRequest
{
    public class CreateFoodPlanRequest
    {
        public string Name {get; set;}
        public double Calories {get; set;}
        public double Protein {get; set;}
        public double Carbohydrate  {get; set;}
        public double Fat {get; set;}
        public double Fiber {get; set;} // NOVO
        public string Observations {get; set;}
        public DateTime InitDate { get; set;}
        public DateTime? EndDate {get; set;}
        public Guid NutritionistId {get; set;}
        public Guid PatientId {get; set;}
        public bool IsActive {get; set;}
        public List<CreateMealRequest> Meals {get; set;} = new(); // NOVO
    }
}