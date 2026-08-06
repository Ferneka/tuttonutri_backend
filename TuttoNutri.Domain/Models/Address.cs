using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class Address : BaseModel, IAggregateRoot
    {
        public string Name {get; private set;}
        public string Addresses {get; private set;}
        public string Number {get; private set;}
        public string City {get; private set;}
        public string Cep {get; private set;}
        public string Country {get; private set;}
        public Address(string name, string addresses, string number, string city, string cep, string country )
        {
            Name = name;
            Addresses = addresses;
            Number = number;
            City = city;
            Cep = cep;
            Country = country;
        }
        public void Update(string name, string addresses, string number, string city, string cep, string country)
        {
            Name = name;
            Addresses = addresses;
            Number = number;
            City = city;
            Cep = cep;
            Country = country;
        }
    }
}