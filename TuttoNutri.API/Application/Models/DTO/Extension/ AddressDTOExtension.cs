using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class AddressDTOExtension
    {
        public static AddressDTO ToDTO(this Address address)
        {
            if (address is null) return null;

            return new AddressDTO
            {
                Id = address.Id,
                Name = address.Name,
                Addresses = address.Addresses,
                Number = address.Number,
                City = address.City,
                Cep = address.Cep,
                Country = address.Country,
                IsActive = address.IsActive
            };
        }
    }
}