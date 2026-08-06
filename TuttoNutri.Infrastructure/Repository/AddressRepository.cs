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
    public class AddressRepository(ApplicationDataContext context, IUnitOfWork unitOfWork) : IAddressRepository
    {
        private readonly ApplicationDataContext _context = context;
        private readonly DbSet<Address> _entity = context.Set<Address>();
        public IUnitOfWork UnitOfWork => unitOfWork;
        public void Add(Address entity)
        {
            _entity.Add(entity);
        }

        public async Task DeactivateAsync(Guid id)
        {
            var address = await _entity.FindAsync(id);
        
            if (address is null) return;

            address.Deactivate(); 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Address>> GetAll()
        {
            return await _entity
            .Where(a => a.IsActive == true)
            .ToListAsync();
        }

        public async Task<Address> GetById(Guid id)
        {
            return await _entity
            .Where(a => a.IsActive == true)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Update(Address entity)
        {
            _entity.Update(entity);
        }
    }
}