using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.Domain.Models
{
    public sealed class Meal : BaseModel
    {
        public string Name { get; private set; }        // "Café da manhã"
        public TimeSpan Time { get; private set; }        
        public Guid FoodPlanId { get; private set; }
        public FoodPlan FoodPlan { get; private set; }
        public ICollection<MealItem> Items { get; private set; }
    }
}