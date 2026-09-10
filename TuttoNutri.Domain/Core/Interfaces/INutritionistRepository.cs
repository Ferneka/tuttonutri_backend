using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Domain.Core.Interfaces
{
    public interface INutritionistRepository : IRepository<Nutritionist, Guid>
    {
        Task<List<Nutritionist>> GetAll();
        Task DeactivateAsync(Guid id);
        Task<Nutritionist> GetByUserId(string userId);
        Task<Nutritionist> GetByCrn(string crn);
        void Update(Nutritionist nutritionist);
        
    }
}