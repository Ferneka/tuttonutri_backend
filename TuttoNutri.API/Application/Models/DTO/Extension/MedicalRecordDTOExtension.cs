using System;
using TuttoNutri.Domain.Enum;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.API.Application.Models.DTO.Extension
{
    public static class MedicalRecordDTOExtension
    {
        public static MedicalRecordDTO ToDTO(this MedicalRecord mr) => new()
        {
            Id = mr.Id,
            PatientId = mr.PatientId,
            NutritionistId = mr.NutritionistId,
            ConsultationId = mr.ConsultationId,
            EvaluationDate = mr.EvaluationDate,
            ReferenciaComposicao = mr.ReferenciaComposicao,
            ObservacoesClinicas = mr.ObservacoesClinicas,
            Weight = mr.Weight,
            Height = mr.Height,
            BodyFat = mr.BodyFat,
            MuscleMass = mr.MuscleMass,
            Objective = mr.Objective,
            CircPescoco = mr.CircPescoco,
            CircTorax = mr.CircTorax,
            CircCintura = mr.CircCintura,
            CircAbdomen = mr.CircAbdomen,
            CircQuadril = mr.CircQuadril,
            CircBracoRelaxado = mr.CircBracoRelaxado,
            CircBracoContraido = mr.CircBracoContraido,
            CircAntebraco = mr.CircAntebraco,
            CircCoxaProximal = mr.CircCoxaProximal,
            CircPanturrilha = mr.CircPanturrilha,
            DobraTriceps = mr.DobraTriceps,
            DobraSubescapular = mr.DobraSubescapular,
            DobraAxilarMedia = mr.DobraAxilarMedia,
            DobraPeitoral = mr.DobraPeitoral,
            DobraSupraIliaca = mr.DobraSupraIliaca,
            DobraAbdominal = mr.DobraAbdominal,
            DobraCoxa = mr.DobraCoxa,
            DiamPunho = mr.DiamPunho,
            DiamFemur = mr.DiamFemur,
            DiamUmero = mr.DiamUmero,

            // Patient/Nutritionist/Consultation ficam null aqui de propósito — mapear a
            // entidade inteira dentro do DTO pode causar referência circular no JSON
            // (Patient -> Nutritionist -> Patients -> ...) e expõe campos internos que o
            // frontend não deveria ver. Se precisar do nome do paciente, por exemplo,
            // é mais seguro adicionar um campo simples tipo `PatientName` no DTO.
        };

        public static string ToWireString(this ReferenciaComposicao referencia) =>
            referencia == ReferenciaComposicao.JP7 ? "jp7" : "nenhuma";

        public static ReferenciaComposicao ParseReferenciaComposicao(this string referencia) => referencia switch
        {
            "jp7" => ReferenciaComposicao.JP7,
            "nenhuma" or null or "" => ReferenciaComposicao.Nenhuma,
            _ => throw new InvalidOperationException($"Referência de composição inválida: '{referencia}'."),
        };
    }
}