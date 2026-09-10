using System;
using System.Collections.Generic;
using TuttoNutri.API.Application.Models.Request.MealRequest; // ajuste se o namespace real for outro

namespace TuttoNutri.API.Application.Models.Request.FoodPlanRequest
{
    // ATENÇÃO: eu não vi o arquivo original desse Request — montei ele batendo
    // com os campos que o FoodPlanService.Update() já usava. Se o seu tiver
    // campos a mais (ex: validações, atributos), mescla com o original em vez
    // de sobrescrever inteiro.
    public class UpdateFoodPlanRequest
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public double Calories {get; set;}
        public double Protein {get; set;}
        public double Carbohydrate  {get; set;}
        public double Fat {get; set;}
        public double Fiber {get; set;} 
        public string Observations {get; set;}
        public DateTime InitDate { get; set;}
        public DateTime? EndDate {get; set;}
        public Guid NutritionistId {get; set;}
        public Guid PatientId {get; set;}
        public bool IsActive {get; set;}
        public List<CreateMealRequest> Meals {get; set;} = new(); 
    }
}