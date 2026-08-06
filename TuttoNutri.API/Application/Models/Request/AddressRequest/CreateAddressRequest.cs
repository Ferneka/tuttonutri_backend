using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.AddressRequest
{
    public class CreateAddressRequest
    {
        public string Name {get; set;}
        public string Addresses {get; set;}
        public string Number {get; set;}
        public string City {get; set;}
        public string Cep {get; set;}
        public string Country {get; set;}
    }
}