using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.MealRequest
{
    public class CreateMealFoodItemRequest
    {
        public int TacoId { get; set; }
        public string Description { get; set; }
        public double Grams { get; set; }
        public double KcalPer100g { get; set; }
        public double ProteinPer100g { get; set; }
        public double FatPer100g { get; set; }
        public double CarbohydratePer100g { get; set; }
        public double FiberPer100g { get; set; }
    }
}