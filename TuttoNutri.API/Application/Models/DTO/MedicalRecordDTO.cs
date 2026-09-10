using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Enum;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO
{
    public class MedicalRecordDTO
    {
        public Guid Id {get; set;}
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid NutritionistId { get; set; }
        public Nutritionist Nutritionist { get; set; }
        public Guid? ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
        public DateTime EvaluationDate { get; set; }
        public ReferenciaComposicao ReferenciaComposicao { get; set; }
        public string ObservacoesClinicas { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? BodyFat { get; set; }
        public double? MuscleMass { get; set; }
        public string Objective { get; set; }
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
        public double? DiamPunho { get;  set; }
        public double? DiamFemur { get; set; }
        public double? DiamUmero { get; set; }

       
    }
}