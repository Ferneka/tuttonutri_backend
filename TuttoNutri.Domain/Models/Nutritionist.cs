using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class Nutritionist : BaseModel, IAggregateRoot
    {
        public string UserId { get; private set; }
        public User User { get; private set; }
        public string Crn { get; private set; }          

        // public Guid? AddressId { get; private set; }
        // public Address Address { get; private set; }
        // public ICollection<Patient> Patients { get; private set; }
        // public ICollection<Consultation> Consultation { get; set; }          
        // public ICollection<FoodPlan> FoodPlan { get; set; }
        public Nutritionist(string userId, string crn)
        {
            UserId = userId;
            Crn = crn;
        }
        public void Update(Guid userId, string crn)
        {
            UserId = userId.ToString();
            Crn = crn;
        }
        // public void SetAddress(Guid addressId)
        // {
        //     AddressId = addressId;
        // }
    }
}