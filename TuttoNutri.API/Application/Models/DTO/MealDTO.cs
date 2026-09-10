using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan? Time { get; set; }
        public List<MealFoodItemDTO> Items { get; set; } = new();
    }
}