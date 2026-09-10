using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class FoodPlanDTO
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public double Calories {get; set;}
        public double Protein {get; set;}
        public double Carbohydrate  {get; set;}
        public double Fat {get; set;}
        public double Fiber {get; set;} // NOVO
        public string Observations {get; set;}
        public DateTime InitDate { get; set;}
        public DateTime? EndDate {get; set;}
        public NutritionistDTO Nutritionist {get; set;}
        public Guid PatientId {get; set;} // NOVO — só o id, não precisa do Patient inteiro
       // public PatientDTO Patient {get; set;}
        public bool IsActive {get; set;}
        public List<MealDTO> Meals {get; set;} = new(); // NOVO
    }
}