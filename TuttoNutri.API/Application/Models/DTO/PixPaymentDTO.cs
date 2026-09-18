using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class PixPaymentDTO
    {
        public decimal Amount { get; set; }
        public string Plano { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}