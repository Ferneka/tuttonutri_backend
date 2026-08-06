using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class User : IdentityUser
    {
        public string Name { get; private set; }
        public bool Profile { get; private set; } = false;
        public Nutritionist Nutritionist { get; private set; }

        public User(string name, string email)
        {
            Name = name;
            UserName = email;
            Email = email;
            Profile = true;
        }
    }
}