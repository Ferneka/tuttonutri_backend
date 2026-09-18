using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.Domain.Models
{
    public class EmailChangeRequest
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string NewEmail { get; set; }
        public string Code { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
    }
}