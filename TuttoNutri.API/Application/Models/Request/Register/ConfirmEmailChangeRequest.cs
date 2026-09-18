using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.Request.Register
{
    public class ConfirmEmailChangeRequest
    {
        public string Code { get; set; }
    }
}