using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.AddressRequest;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        public AddressService(IAddressRepository repository)
        {
            _repository = repository;
        }
            public async Task<bool> Add(CreateAddressRequest request)
            {
                var address = new Address(
                    request.Name,
                    request.Addresses,
                    request.Number,
                    request.City,
                    request.Cep,
                    request.Country
                );
    
            _repository.Add(address);
    
                return await _repository.UnitOfWork.SaveEntitiesAsync();
            }

        public async Task<bool> Delete(Guid id)
        {
            var address = await _repository.GetById(id);
 
            if (address is null) return false;
 
            address.Deactivate();
 
            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<AddressDTO>> GetAll()
        { 
            var data = await _repository.GetAll();

            return data.Select(p => p.ToDTO()).ToList();
        }

        public async Task<AddressDTO> GetById(Guid id)
        {
            var address = await _repository.GetById(id);
 
            if (address is null) return null;

            return address.ToDTO();
           
        }

        public async Task<bool> Update(UpdateAddressRequest request)
        {
            var address = await _repository.GetById(request.Id);
 
            if (address is null) return false;
 
            address.Update(
                request.Name,
                request.Addresses,
                request.Number,
                request.City,
                request.Cep,
                request.Country
            );
 
            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}