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
            .Include(f => f.Meals).ThenInclude(m => m.Items) // NOVO
            .Where(f => f.IsActive == true)
            .ToListAsync();
        }

        public async Task<FoodPlan> GetById(Guid id)
        {
           return await _entity
            //.Include(f => f.Patient).ThenInclude(p => p.Address)
            .Include(f => f.Nutritionist).ThenInclude(n => n.User)
            //.Include(f => f.Nutritionist).ThenInclude(n => n.Address)
            .Include(f => f.Meals).ThenInclude(m => m.Items) // NOVO — essencial: sem isso o Update() não enxerga as refeições antigas pra substituir
            .Where(f => f.IsActive == true)
            .FirstOrDefaultAsync(f => f.Id == id);
        }

        public void Update(FoodPlan entity, HashSet<Guid> originalMealIds, HashSet<Guid> originalItemIds)
        {
        
            _context.Entry(entity).State = EntityState.Modified;

            var currentMealIds = entity.Meals.Select(m => m.Id).ToHashSet();

            // Refeições que existiam antes e sumiram -> deletar
            foreach (var oldId in originalMealIds.Except(currentMealIds))
            {
                var meal = _context.ChangeTracker.Entries<Meal>()
                    .FirstOrDefault(e => e.Entity.Id == oldId)?.Entity;
                if (meal != null) _context.Entry(meal).State = EntityState.Deleted;
            }

            foreach (var meal in entity.Meals)
            {
                // não existia antes -> força Added, ignorando o que o EF decidiu sozinho
                if (!originalMealIds.Contains(meal.Id))
                    _context.Entry(meal).State = EntityState.Added;

                var currentItemIds = meal.Items.Select(i => i.Id).ToHashSet();

                foreach (var item in meal.Items)
                {
                    if (!originalItemIds.Contains(item.Id))
                        _context.Entry(item).State = EntityState.Added;
                }
            }

            var allCurrentItemIds = entity.Meals.SelectMany(m => m.Items).Select(i => i.Id).ToHashSet();
            foreach (var oldItemId in originalItemIds.Except(allCurrentItemIds))
            {
                var item = _context.ChangeTracker.Entries<MealFoodItem>()
                    .FirstOrDefault(e => e.Entity.Id == oldItemId)?.Entity;
                if (item != null) _context.Entry(item).State = EntityState.Deleted;
            }
                    }
                }
}

