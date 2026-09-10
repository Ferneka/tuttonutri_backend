using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TuttoNutri.API.Application.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            try
            {
                using var client = new SmtpClient(_settings.Host, _settings.Port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_settings.User, _settings.Password),
                    EnableSsl = true,
                    Timeout = 10000 // 10s — evita a requisição travar até o Azure derrubar a conexão
                };

                using var message = new MailMessage(_settings.From, to, subject, body);

                await client.SendMailAsync(message);

                _logger.LogInformation("E-mail enviado com sucesso para {Email}", to);
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "Erro SMTP ao enviar e-mail para {Email}. Host: {Host}, Porta: {Port}",
                    to, _settings.Host, _settings.Port);
                throw; // quem chamou decide se trata ou não
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao enviar e-mail para {Email}", to);
                throw;
            }
        }
    }
}