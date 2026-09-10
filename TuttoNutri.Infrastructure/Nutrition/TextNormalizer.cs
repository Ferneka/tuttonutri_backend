using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuttoNutri.Infrastructure.Nutrition
{
    public static class TextNormalizer
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
    
            var formD = input.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
    
            foreach (var c in formD)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
    
            var semAcento = sb.ToString().Normalize(NormalizationForm.FormC);
    
            var limpo = new string(semAcento.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray());
            return limpo.Trim();
        }
    }
}