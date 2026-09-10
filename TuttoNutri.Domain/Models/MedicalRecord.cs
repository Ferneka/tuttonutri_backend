using System;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Enum;

namespace TuttoNutri.Domain.Models
{
    public sealed class MedicalRecord : BaseModel, IAggregateRoot
    {
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }
        public Guid? ConsultationId { get; private set; }
        public Consultation Consultation { get; private set; }
        public DateTime EvaluationDate { get; private set; }
        public ReferenciaComposicao ReferenciaComposicao { get; private set; }
        public string ObservacoesClinicas { get; private set; }
        public double? Weight { get; private set; }
        public double? Height { get; private set; }
        public double? BodyFat { get; private set; }
        public double? MuscleMass { get; private set; }
        public string Objective { get; private set; }
        public double? CircPescoco { get; private set; }
        public double? CircTorax { get; private set; }
        public double? CircCintura { get; private set; }
        public double? CircAbdomen { get; private set; }
        public double? CircQuadril { get; private set; }
        public double? CircBracoRelaxado { get; private set; }
        public double? CircBracoContraido { get; private set; }
        public double? CircAntebraco { get; private set; }
        public double? CircCoxaProximal { get; private set; }
        public double? CircPanturrilha { get; private set; }
        public double? DobraTriceps { get; private set; }
        public double? DobraSubescapular { get; private set; }
        public double? DobraAxilarMedia { get; private set; }
        public double? DobraPeitoral { get; private set; }
        public double? DobraSupraIliaca { get; private set; }
        public double? DobraAbdominal { get; private set; }
        public double? DobraCoxa { get; private set; }
        public double? DiamPunho { get; private set; }
        public double? DiamFemur { get; private set; }
        public double? DiamUmero { get; private set; }

        public Guid? FoodPlanId { get; private set; }
        public FoodPlan FoodPlan { get; private set; }

        private MedicalRecord() { } 

        public MedicalRecord(
            Guid patientId, Guid nutritionistId, DateTime evaluationDate,
            string observacoesClinicas = null)
        {
            PatientId = patientId;
            NutritionistId = nutritionistId;
            EvaluationDate = evaluationDate;
            ObservacoesClinicas = observacoesClinicas;
            ReferenciaComposicao = ReferenciaComposicao.Nenhuma;
        }

        public void Update(string observacoesClinicas)
        {
            ObservacoesClinicas = observacoesClinicas;
        }

        public void VincularConsulta(Guid consultationId)
        {
            ConsultationId = consultationId;
        }

        public void SetMeasurements(double? weight, double? height, double? bodyFat, double? muscleMass, string objective)
        {
            Weight = weight;
            Height = height;
            BodyFat = bodyFat;
            MuscleMass = muscleMass;
            Objective = objective;
        }

        public void AtualizarDadosGerais(DateTime evaluationDate, ReferenciaComposicao referenciaComposicao, double? weight, double? heightSnapshot)
        {
            EvaluationDate = evaluationDate;
            ReferenciaComposicao = referenciaComposicao;
            Weight = weight;
            Height = heightSnapshot;
        }

        public void AtualizarCircunferencias(
            double? pescoco, double? torax, double? cintura, double? abdomen, double? quadril,
            double? bracoRelaxado, double? bracoContraido, double? antebraco,
            double? coxaProximal, double? panturrilha)
        {
            CircPescoco = pescoco;
            CircTorax = torax;
            CircCintura = cintura;
            CircAbdomen = abdomen;
            CircQuadril = quadril;
            CircBracoRelaxado = bracoRelaxado;
            CircBracoContraido = bracoContraido;
            CircAntebraco = antebraco;
            CircCoxaProximal = coxaProximal;
            CircPanturrilha = panturrilha;
        }

        public void AtualizarDobras(
            double? triceps, double? subescapular, double? axilarMedia, double? peitoral,
            double? supraIliaca, double? abdominal, double? coxa)
        {
            DobraTriceps = triceps;
            DobraSubescapular = subescapular;
            DobraAxilarMedia = axilarMedia;
            DobraPeitoral = peitoral;
            DobraSupraIliaca = supraIliaca;
            DobraAbdominal = abdominal;
            DobraCoxa = coxa;
        }

        public void AtualizarDiametros(double? punho, double? femur, double? umero)
        {
            DiamPunho = punho;
            DiamFemur = femur;
            DiamUmero = umero;
        }

        public void SetFoodPlan(Guid foodPlanId)
        {
            FoodPlanId = foodPlanId;
        }
    }
}