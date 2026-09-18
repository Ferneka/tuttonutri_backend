using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.NutritionistRequest
{
    public class UpdateNutritionistRequest
    {
         public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthOfDate { get; set; }

    }
}