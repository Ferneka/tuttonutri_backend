using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.PatientRequest;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Activate(Guid id, Guid nutritionistId)
        {
            var patient = await _repository.GetById(id);
            if (patient == null || patient.NutritionistId != nutritionistId) return false;

            var result = await _repository.Activate(id);
            if (result) await _repository.UnitOfWork.SaveEntitiesAsync(); 

            return result;
        }

        public async Task<PatientDTO> Create(CreatePatientRequest request, Guid nutritionistId)
        {
             var patient = new Patient(
                request.Name,
                request.Phone,
                request.Gender,
                request.BirthOfDate,
                request.Height,
                nutritionistId
            );

            _repository.Add(patient);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return patient.ToDTO();
        }

        public async Task<bool> Deactivate(Guid id, Guid nutritionistId)
        {
            var patient = await _repository.GetById(id);

            if (patient is null || patient.NutritionistId != nutritionistId) return false;

            patient.Deactivate();

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<PatientDTO>> GetAll(Guid nutritionistId)
        {
            var data = await _repository.GetAll();

            return data
                .Where(p => p.NutritionistId ==  nutritionistId)
                .Select(n => n.ToDTO())
                .ToList();
        }

       public async Task<PatientDTO> GetById(Guid id, Guid nutritionistId)
        {
            var patient = await _repository.GetById(id);

            if (patient is null || patient.NutritionistId != nutritionistId) return null;

            return patient.ToDTO();
        }

        public async Task<bool> Update(UpdatePatientRequest request, Guid nutritionistId)
        {
            var patient = await _repository.GetById(request.Id);

            if (patient is null || patient.NutritionistId != nutritionistId) return false;

            patient.Update(
                request.Name,
                request.Phone,
                request.Gender,
                request.BirthOfDate,
                request.Height
            );

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
                
    }
}