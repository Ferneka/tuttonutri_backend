using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.AddressRequest;

namespace TuttoNutri.API.Application.Services.AddressService
{
    public interface IAddressService
    {
        Task<bool> Add(CreateAddressRequest request);
        Task<AddressDTO> GetById(Guid id);
        Task<List<AddressDTO>> GetAll();
        Task<bool> Update(UpdateAddressRequest request);
        Task<bool> Delete(Guid id);
    }   
}