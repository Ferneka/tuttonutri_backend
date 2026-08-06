using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.Domain.Models
{
    public sealed class Consultation : BaseModel, IAggregateRoot
    {
        public DateTime Date { get; private set; }
        public string Status { get; private set; }                
        public string Observations  { get; private set; }
        public int MinuteDuration { get; private set; }   
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }
        public MedicalRecord MedicalRecord { get; private set; }
        public Consultation(DateTime date, string status, string observations, int minuteDuration,
            Guid patientId, Guid nutritionistId)
        {
            Date = date;
            Status = status;
            Observations = observations;
            MinuteDuration = minuteDuration;
            PatientId = patientId;
            NutritionistId = nutritionistId;
        }

    public void Update(DateTime date, string status, string observations, int minuteDuration)
    {
        Date = date;
        Status = status;
        Observations = observations;
        MinuteDuration = minuteDuration;
    }
    }
}