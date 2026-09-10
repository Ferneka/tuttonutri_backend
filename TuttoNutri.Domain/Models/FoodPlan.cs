using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class FoodPlan : BaseModel, IAggregateRoot
    {
        public string Name { get; private set; }
        public double Calories { get; private set; }
        public double Protein { get; private set; }
        public double Carbohydrate { get; private set; }
        public double Fat { get; private set; }
        public double Fiber { get; private set; } // NOVO
        public string Observations { get; private set; }
        public DateTime InitDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }

        // NOVO: uma FoodPlan tem várias Meals (refeições)
        private readonly List<Meal> _meals = new();
        public IReadOnlyCollection<Meal> Meals => _meals.AsReadOnly();

        public FoodPlan(string name, double calories, double protein, double carbohydrate, double fat, double fiber,
        string observations, DateTime initDate, DateTime? endDate, Guid patientId, Guid nutritionistId)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Fiber = fiber;
            Observations = observations;
            InitDate = initDate;
            EndDate = endDate;
            PatientId = patientId;
            NutritionistId = nutritionistId;
        }

        public void Update(string name, double calories, double protein, double carbohydrate, double fat, double fiber,
        string observations, DateTime initDate, DateTime endDate, Guid patientId, Guid nutritionistId, bool isActive)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Fiber = fiber;
            Observations = observations;
            InitDate = initDate;
            EndDate = endDate;
            PatientId = patientId;
            NutritionistId = nutritionistId;
            IsActive = isActive;
        }

        // NOVO: métodos pra gerenciar as refeições sem quebrar o encapsulamento
        public Meal AddMeal(string name, TimeSpan? time)
        {
            var meal = new Meal(name, time, Id);
            _meals.Add(meal);
            return meal;
        }

        public void RemoveMeal(Guid mealId)
        {
            var meal = _meals.FirstOrDefault(m => m.Id == mealId);
            if (meal != null) _meals.Remove(meal);
        }

        public void ClearMeals()
        {
            _meals.Clear();
        }
    }
}