using System;
using TuttoNutri.API.Application.Models.Request.AddressRequest;

namespace TuttoNutri.API.Application.Models.Request.Register
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Crn { get; set; }
    }
}