using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.FoodPlanRequest;
using TuttoNutri.API.Application.Models.Request.MealRequest; // ajuste se o nome real da pasta/namespace for outro
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Repository;

namespace TuttoNutri.API.Application.Services.FoodPlanService
{
    public class FoodPlanService : IFoodPlanService
    {
        private readonly IFoodPlanRepository _repository;
        public FoodPlanService(IFoodPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<FoodPlanDTO> Add(CreateFoodPlanRequest request)
        {
            var foodPlan = new FoodPlan(
            request.Name,
            request.Calories,
            request.Protein,
            request.Carbohydrate,
            request.Fat,
            request.Fiber,
            request.Observations,
            request.InitDate,
            request.EndDate,
            request.PatientId,
            request.NutritionistId);

            AdicionarRefeicoes(foodPlan, request.Meals);

            _repository.Add(foodPlan);

            var salvou = await _repository.UnitOfWork.SaveEntitiesAsync();

            return salvou ? foodPlan.ToDTO() : null;
        }

        public async Task<bool> Deactivate(Guid id)
        {
            var foodPlan = await _repository.GetById(id);

            if (foodPlan is null) return false;

            foodPlan.Deactivate();

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<FoodPlanDTO>> GetAll()
        {
            var data = await _repository.GetAll();

            return data.Select(f => f.ToDTO()).ToList();
        }

        public async Task<FoodPlanDTO> GetById(Guid id)
        {
            var foodPlan = await _repository.GetById(id);

            if (foodPlan is null) return null;

            return foodPlan.ToDTO();
        }

        public async Task<bool> Update(UpdateFoodPlanRequest request)
        {
            Console.WriteLine($"### SERVICE UPDATE — request.Id: {request.Id}, Meals no request: {request.Meals?.Count ?? -1}");
            var foodPlan = await _repository.GetById(request.Id);
            Console.WriteLine($"### SERVICE UPDATE — GetById retornou: {(foodPlan == null ? "NULL" : foodPlan.Id.ToString())}");

            if (foodPlan is null) return false;

            var originalMealIds = foodPlan.Meals.Select(m => m.Id).ToHashSet();
            var originalItemIds = foodPlan.Meals.SelectMany(m => m.Items).Select(i => i.Id).ToHashSet();

            foodPlan.Update(
                request.Name, request.Calories, request.Protein, request.Carbohydrate,
                request.Fat, request.Fiber, request.Observations, request.InitDate,
                request.EndDate.GetValueOrDefault(), request.PatientId, request.NutritionistId,
                request.IsActive);

            foodPlan.ClearMeals();
            AdicionarRefeicoes(foodPlan, request.Meals);

            _repository.Update(foodPlan, originalMealIds, originalItemIds);

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        private static void AdicionarRefeicoes(FoodPlan foodPlan, List<CreateMealRequest> meals)
        {
            foreach (var mealRequest in meals)
            {
                var meal = foodPlan.AddMeal(mealRequest.Name, mealRequest.Time);

                foreach (var itemRequest in mealRequest.Items)
                {
                    meal.AddItem(
                        itemRequest.TacoId,
                        itemRequest.Description,
                        itemRequest.Grams,
                        itemRequest.KcalPer100g,
                        itemRequest.ProteinPer100g,
                        itemRequest.FatPer100g,
                        itemRequest.CarbohydratePer100g,
                        itemRequest.FiberPer100g);
                }
            }
        }
    }
}