using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.Domain.Models
{
    public class EmailVerificationCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
    }
}