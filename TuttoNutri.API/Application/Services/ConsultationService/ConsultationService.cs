using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.ConsultationRequest;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TuttoNutri.API.Application.Services.ConsultationService
{
    public class ConsultationService : IConsultationService
    {
        private readonly IConsultationRepository _repository;
        private readonly IPatientRepository _patientRepository;

        public ConsultationService(IConsultationRepository repository, IPatientRepository patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        public async Task<ConsultationDTO> Create(CreateConsultationRequest request, Guid nutritionistId)
        {
             var patient = await _patientRepository.GetById(request.PatientId);

            if (patient is null || patient.NutritionistId != nutritionistId)
                throw new InvalidOperationException("Paciente não encontrado.");

            if (patient.IsActive is false)
                throw new InvalidOperationException("Não é possível criar consulta para paciente inativo.");

            var consultation = new Consultation(
                request.Date,
                request.Status,
                request.PatientId,
                nutritionistId
            );

            _repository.Add(consultation);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return consultation.ToDTO();
        }

        public async Task<bool> Deactivate(Guid id, Guid nutritionistId)
        {
            var consultation = await _repository.GetById(id);

            if (consultation is null || consultation.NutritionistId != nutritionistId) return false;

            consultation.Deactivate();

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<ConsultationDTO>> GetAll(Guid nutritionistId)
        {
            var data = await _repository.GetAll();

            return data
                .Where(c => c.NutritionistId == nutritionistId)
                .Select(c => c.ToDTO())
                .ToList();
        }

        public async Task<ConsultationDTO> GetById(Guid id, Guid nutritionistId)
        {
            var consultation = await _repository.GetById(id);

            if (consultation is null || consultation.NutritionistId != nutritionistId) return null;

            return consultation.ToDTO();
        }

        public async Task<bool> Update(UpdateConsultationRequest request, Guid nutritionistId)
        {
           var consultation = await _repository.GetById(request.Id);

            if (consultation is null || consultation.NutritionistId != nutritionistId) return false;

            consultation.Update(request.Date, request.Status);

            await _repository.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}