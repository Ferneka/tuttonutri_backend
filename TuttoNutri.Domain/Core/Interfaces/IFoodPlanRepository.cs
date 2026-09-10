using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface IFoodPlanRepository : IRepository<FoodPlan, Guid>
    {
        Task<List<FoodPlan>> GetAll();
        Task DeactivateAsync(Guid id);
        void Update(FoodPlan foodPlan, HashSet<Guid> originalMealIds, HashSet<Guid> originalItemIds);
    }
}