using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class MpPaymentResponse
    {
         [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } 

        [JsonPropertyName("status_detail")]
        public string StatusDetail { get; set; }

        [JsonPropertyName("transaction_amount")]
        public decimal TransactionAmount { get; set; }

        [JsonPropertyName("point_of_interaction")]
        public PointOfInteraction PointOfInteraction { get; set; }

        [JsonPropertyName("external_reference")]
        public Guid ExternalReference { get; set; }
         
    }
    public class PointOfInteraction
    {
        [JsonPropertyName("transaction_data")]
        public TransactionData TransactionData { get; set; }
    }
    public class TransactionData
    {
        [JsonPropertyName("qr_code")]
        public string QrCode { get; set; } // "copia e cola" do PIX

        [JsonPropertyName("qr_code_base64")]
        public string QrCodeBase64 { get; set; } // imagem do QR code
    }
}