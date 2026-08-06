using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.Domain.Models
{
    public sealed class MealItem : BaseModel
    {
        public string Description { get; private set; }  // "2 ovos mexidos"
        public Guid MealId { get; private set; }
        public Meal Meal { get; private set; }
    }
}