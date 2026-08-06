using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.FoodPlanRequest;
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

        public async Task<bool> Add(CreateFoodPlanRequest request)
        {
            var foodPlan = new FoodPlan(
            request.Name,
            request.Calories,
            request.Protein,
            request.Carbohydrate,
            request.Fat,
            request.Observations,
            request.InitDate,
            request.EndDate,
            request.PatientId,
            request.NutritionistId);

            _repository.Add(foodPlan);

            return await _repository.UnitOfWork.SaveEntitiesAsync();
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
            var foodPlan = await _repository.GetById(request.Id);

            if (foodPlan is null) return false;

            foodPlan.Update(
            request.Name,
            request.Calories,
            request.Protein,
            request.Carbohydrate,
            request.Fat,
            request.Observations,
            request.InitDate,
            request.EndDate.GetValueOrDefault(),
            request.PatientId,        
            request.NutritionistId,  
            request.IsActive);

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}