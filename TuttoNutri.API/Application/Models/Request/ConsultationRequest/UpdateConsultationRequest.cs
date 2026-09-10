using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TuttoNutri.API.Converters;
using TuttoNutri.Domain.Enum;

namespace TuttoNutri.API.Application.Models.Request.ConsultationRequest
{
    public class UpdateConsultationRequest
    {
         public Guid Id { get; set; }
 
        [JsonConverter(typeof(DateTimeWithTimeJsonConverter))]
        public DateTime Date { get; set; }

        public ConsultationStatus Status { get; set; }
    }
}