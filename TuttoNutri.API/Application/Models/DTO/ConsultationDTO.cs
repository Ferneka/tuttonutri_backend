using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TuttoNutri.API.Converters;
using TuttoNutri.Domain.Enum;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class ConsultationDTO
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(DateTimeWithTimeJsonConverter))]
        public DateTime Date { get; set; }
        public ConsultationStatus Status { get; set; }
        public Guid PatientId { get; set; }
        public Guid NutritionistId { get; set; }
        public bool IsActive { get; set; }
    }
}