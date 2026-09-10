using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TuttoNutri.API.Converters;

namespace TuttoNutri.API.Application.Models.Request.MedicalRecordRequest
{
    public class UpdateMedicalRecordRequest
    {
        public Guid Id { get; set; }
 
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Data { get; set; }
        public double? Peso { get; set; } 
        public string ReferenciaComposicao { get; set; } 
        public double? CircPescoco { get; set; }
        public double? CircTorax { get; set; }
        public double? CircCintura { get; set; }
        public double? CircAbdomen { get; set; }
        public double? CircQuadril { get; set; }
        public double? CircBracoRelaxado { get; set; }
        public double? CircBracoContraido { get; set; }
        public double? CircAntebraco { get; set; }
        public double? CircCoxaProximal { get; set; }
        public double? CircPanturrilha { get; set; } 
        public double? DobraTriceps { get; set; }
        public double? DobraSubescapular { get; set; }
        public double? DobraAxilarMedia { get; set; }
        public double? DobraPeitoral { get; set; }
        public double? DobraSupraIliaca { get; set; }
        public double? DobraAbdominal { get; set; }
        public double? DobraCoxa { get; set; } 
        public double? DiamPunho { get; set; }
        public double? DiamFemur { get; set; }
        public double? DiamUmero { get; set; }
    }
}