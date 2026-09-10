using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.MealRequest
{
    public class CreateMealRequest
    {
        public string Name { get; set; }
        public TimeSpan? Time { get; set; }
        public List<CreateMealFoodItemRequest> Items { get; set; } = new();
    }
}