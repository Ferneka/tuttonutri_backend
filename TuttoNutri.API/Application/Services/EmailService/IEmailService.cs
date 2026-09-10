using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Services.EmailService
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}