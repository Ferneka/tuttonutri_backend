using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using TuttoNutri.Infrastructure.Context;

namespace TuttoNutri.Infrastructure.Repository
{
    public class FoodPlanRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : IFoodPlanRepository
    {
        private readonly ApplicationDataContext _context = context;
        private DbSet<FoodPlan> _entity = context.Set<FoodPlan>();
        public IUnitOfWork UnitOfWork => unitOfWork;

        public void Add(FoodPlan entity)
        {
            _entity.Add(entity);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var foodPlan = await _entity.FindAsync(id);
        
            if (foodPlan is null) return;

            foodPlan.Deactivate(); 
            await _context.SaveChangesAsync();
        }

        public async Task<List<FoodPlan>> GetAll()
        {
             return await _entity
            //.Include(f => f.Patient).ThenInclude(p => p.Address)
            .Include(f => f.Nutritionist).ThenInclude(n => n.User)
            //.Include(f => f.Nutritionist).ThenInclude(n => n.Address)
            .Where(f => f.IsActive == true)
            .ToListAsync();
        }

        public async Task<FoodPlan> GetById(Guid id)
        {
           return await _entity
            //.Include(f => f.Patient).ThenInclude(p => p.Address)
            .Include(f => f.Nutritionist).ThenInclude(n => n.User)
            //.Include(f => f.Nutritionist).ThenInclude(n => n.Address)
            .Where(f => f.IsActive == true)
            .FirstOrDefaultAsync(f => f.Id == id);
        }

        public void Update(FoodPlan entity)
        {
            _entity.Update(entity);
        }
    }
}