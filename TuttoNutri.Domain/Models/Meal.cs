using System;
using System.Collections.Generic;
using System.Linq;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    // ATENÇÃO: ajuste a herança/base se o padrão do projeto for diferente
    // (aqui assumo que BaseModel dá Id: Guid, IsActive: bool — igual FoodPlan)
    public sealed class Meal : BaseModel
    {
        public string Name { get; private set; } // "Café da Manhã", "Almoço", etc
        public TimeSpan? Time { get; private set; }
        public Guid FoodPlanId { get; private set; }
        public FoodPlan FoodPlan { get; private set; }

        private readonly List<MealFoodItem> _items = new();
        public IReadOnlyCollection<MealFoodItem> Items => _items.AsReadOnly();

        // construtor exigido pelo EF Core (não usar diretamente no código)
        private Meal() { }

        public Meal(string name, TimeSpan? time, Guid foodPlanId)
        {
            Name = name;
            Time = time;
            FoodPlanId = foodPlanId;
        }

        public void Update(string name, TimeSpan? time)
        {
            Name = name;
            Time = time;
        }

        public MealFoodItem AddItem(int tacoId, string description, double grams,
            double kcalPer100g, double proteinPer100g, double fatPer100g, double carbohydratePer100g, double fiberPer100g)
        {
            var item = new MealFoodItem(tacoId, description, grams, kcalPer100g, proteinPer100g, fatPer100g, carbohydratePer100g, fiberPer100g, Id);
            _items.Add(item);
            return item;
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item != null) _items.Remove(item);
        }
    }
}