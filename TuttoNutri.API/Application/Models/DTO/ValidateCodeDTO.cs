using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class ValidateCodeDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}