using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Enum;

namespace TuttoNutri.Domain.Models
{
    public sealed class Consultation : BaseModel, IAggregateRoot
    {
        public DateTime Date { get; private set; }
        public ConsultationStatus Status {get; private set;}
        public Guid PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public Guid NutritionistId { get; private set; }
        public Nutritionist Nutritionist { get; private set; }

        public Consultation(DateTime date, ConsultationStatus status, Guid patientId, Guid nutritionistId)
        {
            Date = date;
            Status = status;
            PatientId = patientId;
            NutritionistId = nutritionistId;
        }

        public void Update(DateTime date, ConsultationStatus status)
        {
            Date = date;
            Status = status;
        }
    }
}