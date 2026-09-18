using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Filters;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.API.Controllers
{
    public class PaymentController : DefaultController
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INutritionistRepository _nutritionistRepository;

        public PaymentController(
            IConfiguration config,
            IHttpClientFactory httpClientFactory,
            IUnitOfWork unitOfWork,
            INutritionistRepository nutritionistRepository)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _unitOfWork = unitOfWork;
            _nutritionistRepository = nutritionistRepository;
        }

        private Guid GetNutritionistId()
        {
            var claim = User.FindFirst("NutritionistId")?.Value;
            return Guid.Parse(claim);
        }
        [Authorize(Roles = "Nutritionist")]
        [HttpPost("card")]
        public async Task<IActionResult> PayWithCard([FromBody] CardPaymentDTO dto)
        {
            var nutricionistaId = GetNutritionistId();
            var client = CreateMpClient();
            client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());

            var body = new
            {
                transaction_amount = dto.Amount,
                token = dto.CardToken,
                description = $"Assinatura TuttoNutri - {dto.Plano}",
                installments = 1,
                payment_method_id = dto.PaymentMethodId,
                external_reference = nutricionistaId.ToString(),
                payer = new { email = dto.Email }
            };

            var response = await client.PostAsJsonAsync(
                "https://api.mercadopago.com/v1/payments", body);

            var result = await response.Content.ReadFromJsonAsync<MpPaymentResponse>();

            if (result?.Status == "approved")
                await AtivarAssinaturaAsync(nutricionistaId, dto.Plano);

            return Ok(result);
        }
        [Authorize(Roles = "Nutritionist")]
        [HttpPost("pix")]
        public async Task<IActionResult> PayWithPix([FromBody] PixPaymentDTO dto)
        {
            var nutricionistaId = GetNutritionistId();
            var client = CreateMpClient();
            client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());

            var body = new
            {
                transaction_amount = dto.Amount,
                description = $"Assinatura TuttoNutri - {dto.Plano}",
                payment_method_id = "pix",
                external_reference = nutricionistaId.ToString(),
                payer = new { email = dto.Email, first_name = dto.Nome }
            };

            var response = await client.PostAsJsonAsync(
                "https://api.mercadopago.com/v1/payments", body);

            var result = await response.Content.ReadFromJsonAsync<MpPaymentResponse>();

            return Ok(new
            {
                paymentId = result?.Id,
                qrCodeBase64 = result?.PointOfInteraction?.TransactionData?.QrCodeBase64,
                copiaCola = result?.PointOfInteraction?.TransactionData?.QrCode
            });
        }

        [Authorize(Roles = "Nutritionist")]
        [HttpGet("status/{paymentId}")]
        public async Task<IActionResult> GetStatus(long paymentId)
        {
            var client = CreateMpClient();
            var response = await client.GetAsync($"https://api.mercadopago.com/v1/payments/{paymentId}");
            var result = await response.Content.ReadFromJsonAsync<MpPaymentResponse>();

            if (result.Status == "approved")
                await AtivarAssinaturaAsync(result.ExternalReference, null);

            return Ok(new { status = result.Status });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromQuery] string topic, [FromQuery] string id)
        {
            if (topic == "payment")
            {
                var client = CreateMpClient();
                var response = await client.GetAsync($"https://api.mercadopago.com/v1/payments/{id}");
                var payment = await response.Content.ReadFromJsonAsync<MpPaymentResponse>();

                if (payment.Status == "approved")
                    await AtivarAssinaturaAsync(payment.ExternalReference, null);
            }

            return Ok();
        }

        private HttpClient CreateMpClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config["MercadoPago:AccessToken"]);
            return client;
        }

        private async Task AtivarAssinaturaAsync(Guid nutricionistaId, string plano)
        {
            var nutritionist = await _nutritionistRepository.GetById(nutricionistaId);
            if (nutritionist == null) return;

            if (!string.IsNullOrEmpty(plano))
                nutritionist.PlanoAtivo = plano;

            nutritionist.DataExpiracao = nutritionist.PlanoAtivo == "Anual"
                ? DateTime.UtcNow.AddYears(1)
                : DateTime.UtcNow.AddMonths(1);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}