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
    public class NutritionistRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : INutritionistRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<Nutritionist> _entity = context.Set<Nutritionist>();
        public IUnitOfWork UnitOfWork => unitOfWork;

        public void Add(Nutritionist entity)
        {
            _entity.Add(entity);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var nutritionist = await _entity.FindAsync(id);
        
            if (nutritionist is null) return;

            nutritionist.Deactivate(); 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Nutritionist>> GetAll()
        {
            return await _entity
            //.Include(a => a.Address)
            .Include(a => a.User)
            .Where(a => a.IsActive == true)
            .ToListAsync();
        }

        public async Task<Nutritionist> GetById(Guid id)
        {
            return await _entity
            //.Include(a => a.Address)
            .Include(a => a.User)
            .Where(a => a.IsActive == true)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Nutritionist> GetByUserId(string userId)
        {
            return await _entity.FirstOrDefaultAsync(n => n.UserId == userId);
        }

        public void Update(Nutritionist entity)
        {
            _entity.Update(entity);
        }
    }
}