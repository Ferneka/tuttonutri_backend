using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TuttoNutri.API.Converters;

namespace TuttoNutri.API.Application.Models.Request.MedicalRecordRequest
{
    public class CreateMedicalRecordRequest
    {
        public Guid PatientId { get; set; }
 
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Data { get; set; }
    }
}