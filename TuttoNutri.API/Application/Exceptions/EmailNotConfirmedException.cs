using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Exceptions
{
    public class EmailNotConfirmedException : BusinessException
    {
        public const string CodeValue = "EMAIL_NOT_CONFIRMED";
 
        public EmailNotConfirmedException()
            : base("Confirme seu e-mail antes de fazer login.")
        {
        }
    }
}