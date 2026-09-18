using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class CardPaymentDTO
    {
        public string CardToken { get; set; }
        public string PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Plano { get; set; }
        public string Email { get; set; }
        public int Installments { get; set; } = 1;
    }
}