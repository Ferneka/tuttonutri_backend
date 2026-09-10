using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuttoNutri.Infrastructure.Nutrition
{
    public class TacoFoodItem
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public double? Kcal { get; set; }
        public double? ProteinaG { get; set; }
        public double? GorduraG { get; set; }
        public double? CarboidratoG { get; set; }
        public double? FibraG { get; set; }
    }
}