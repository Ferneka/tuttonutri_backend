using System;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class MedicalRecord : BaseModel, IAggregateRoot
    {
        public Guid ConsultationId { get; private set; }
        public Consultation Consultation { get; private set; }

        public string ObservacoesClinicas { get; private set; }

        public double? Weight { get; private set; }
        public double? Height { get; private set; }
        public double? BodyFat { get; private set; }
        public double? MuscleMass { get; private set; }
        public string Objective { get; private set; }

        public Guid? FoodPlanId { get; private set; }
        public FoodPlan FoodPlan { get; private set; }

        public MedicalRecord(Guid consultationId, string observacoesClinicas)
        {
            ConsultationId = consultationId;
            ObservacoesClinicas = observacoesClinicas;
        }

        public void Update(string observacoesClinicas)
        {
            ObservacoesClinicas = observacoesClinicas;
        }

        public void SetMeasurements(double? weight, double? height, double? bodyFat, double? muscleMass, string objective)
        {
            Weight = weight;
            Height = height;
            BodyFat = bodyFat;
            MuscleMass = muscleMass;
            Objective = objective;
        }

        public void SetFoodPlan(Guid foodPlanId)
        {
            FoodPlanId = foodPlanId;
        }
    }
}