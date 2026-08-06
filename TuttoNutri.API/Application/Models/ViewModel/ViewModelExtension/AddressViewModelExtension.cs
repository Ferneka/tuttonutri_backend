using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;

namespace TuttoNutri.API.Application.Models.ViewModel.ViewModelExtension
{
    public static class AddressViewModelExtension
    {
        public static AddressViewModel ToViewModel(this AddressDTO DTO)
        {
            return new AddressViewModel
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Addresses = DTO.Addresses,
                Number = DTO.Number,
                City = DTO.City,
                Cep = DTO.Cep,
                Country = DTO.Country,
                IsActive = DTO.IsActive
            };
        }
    }
}