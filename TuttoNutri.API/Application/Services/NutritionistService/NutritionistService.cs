using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.DTO.Extension;
using TuttoNutri.API.Application.Models.Request.NutritionistRequest;
using TuttoNutri.Domain.Core.Interfaces;

namespace TuttoNutri.API.Application.Services.NutritionistService
{
    public class NutritionistService : INutritionistService
    {
        private readonly INutritionistRepository _repository;
        public NutritionistService(INutritionistRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Deactivate(Guid id)
        {
            var nutritionist = await _repository.GetById(id);

            if (nutritionist is null) return false;

            nutritionist.Deactivate();

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<NutritionistDTO>> GetAll()
        {
            var data = await _repository.GetAll();

            return data.Select(n => n.ToDTO()).ToList();
        }

        public async Task<NutritionistDTO> GetById(Guid id)
        {
            var nutritionist = await _repository.GetById(id);

            if (nutritionist is null) return null;

            return nutritionist.ToDTO();
        }

        public async Task<bool> Update(UpdateNutritionistRequest request)
        {
            var nutritionist = await _repository.GetById(request.Id);

            if (nutritionist is null) return false;

            nutritionist.UpdateContactInfo(request.Cpf, request.Phone, request.BirthOfDate);

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}