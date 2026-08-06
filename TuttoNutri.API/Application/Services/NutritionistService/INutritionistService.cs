using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuttoNutri.API.Application.Models.DTO;
using TuttoNutri.API.Application.Models.Request.NutritionistRequest;

namespace TuttoNutri.API.Application.Services.NutritionistService
{
    public interface INutritionistService
    {
        Task<NutritionistDTO> GetById(Guid id);
        Task<List<NutritionistDTO>> GetAll();
        Task<bool> Update(UpdateNutritionistRequest request);
        Task<bool> Deactivate(Guid id);
    }
}