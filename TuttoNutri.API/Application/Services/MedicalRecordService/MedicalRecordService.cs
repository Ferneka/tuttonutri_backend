using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.MedicalRecordRequest;
using TuttoNutri.Domain.Core.Interfaces;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Services.MedicalRecordService
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly IPatientRepository _patientRepository;

        public MedicalRecordService(IMedicalRecordRepository repository, IPatientRepository patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        public async Task<MedicalRecordDTO> Create(CreateMedicalRecordRequest request, Guid nutritionistId)
        {
            var patient = await _patientRepository.GetById(request.PatientId);

            if (patient is null || patient.NutritionistId != nutritionistId)
                throw new InvalidOperationException("Paciente não encontrado.");

            if (patient.IsActive is false)
                throw new InvalidOperationException("Não é possível criar avaliação para paciente inativo.");

            var medicalRecord = new MedicalRecord(patient.Id, nutritionistId, request.Data);
            medicalRecord.SetMeasurements(null, patient.Height, null, null, null);

            _repository.Add(medicalRecord);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return await GetById(medicalRecord.Id, nutritionistId);
        }

        public async Task<MedicalRecordDTO> Update(UpdateMedicalRecordRequest request, Guid nutritionistId)
        {
            var medicalRecord = await _repository.GetById(request.Id);

            if (medicalRecord is null || medicalRecord.NutritionistId != nutritionistId)
                throw new InvalidOperationException("Prontuário não encontrado.");

            var patient = await _patientRepository.GetById(medicalRecord.PatientId);

            medicalRecord.AtualizarDadosGerais(
                request.Data,
                request.ReferenciaComposicao.ParseReferenciaComposicao(),
                request.Peso,
                patient?.Height);

            medicalRecord.AtualizarCircunferencias(
                request.CircPescoco, request.CircTorax, request.CircCintura, request.CircAbdomen, request.CircQuadril,
                request.CircBracoRelaxado, request.CircBracoContraido, request.CircAntebraco,
                request.CircCoxaProximal, request.CircPanturrilha);

            medicalRecord.AtualizarDobras(
                request.DobraTriceps, request.DobraSubescapular, request.DobraAxilarMedia, request.DobraPeitoral,
                request.DobraSupraIliaca, request.DobraAbdominal, request.DobraCoxa);

            medicalRecord.AtualizarDiametros(request.DiamPunho, request.DiamFemur, request.DiamUmero);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            return await GetById(medicalRecord.Id, nutritionistId);
        }

        public async Task<MedicalRecordDTO> GetById(Guid id, Guid nutritionistId)
        {
            var medicalRecord = await _repository.GetById(id);

            if (medicalRecord is null || medicalRecord.NutritionistId != nutritionistId) return null;

            return medicalRecord.ToDTO();
        }

        public async Task<List<MedicalRecordDTO>> GetByPatient(Guid patientId, Guid nutritionistId)
        {
            var patient = await _patientRepository.GetById(patientId);

            if (patient is null || patient.NutritionistId != nutritionistId)
                throw new InvalidOperationException("Paciente não encontrado.");

            var registros = await _repository.GetByPatientId(patientId);

            return registros.Select(mr => mr.ToDTO()).ToList();
        }

        public async Task<bool> Deactivate(Guid id, Guid nutritionistId)
        {
            var medicalRecord = await _repository.GetById(id);

            if (medicalRecord is null || medicalRecord.NutritionistId != nutritionistId) return false;

            if (!medicalRecord.IsActive) return true; // já estava inativo, considera sucesso

            medicalRecord.Deactivate();

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}