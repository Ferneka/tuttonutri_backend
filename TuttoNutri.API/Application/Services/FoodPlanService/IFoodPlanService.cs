using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.FoodPlanRequest;

namespace TuttoNutri.API.Application.Services.FoodPlanService
{
    public interface IFoodPlanService
    {
        Task<bool> Add(CreateFoodPlanRequest request);
        Task<FoodPlanDTO> GetById(Guid id);
        Task<List<FoodPlanDTO>> GetAll();
        Task<bool> Update(UpdateFoodPlanRequest request);
        Task<bool> Deactivate(Guid id);
    }
}