using System;

namespace TuttoNutri.Domain.Models
{
    // ATENÇÃO: ajuste a herança/base se o padrão do projeto for diferente
    public sealed class MealFoodItem : BaseModel
    {
        public int TacoId { get; private set; }         // id do alimento na tabela TACO (referência, não FK de banco)
        public string Description { get; private set; }  // descrição da TACO no momento da escolha (ex: "Ovo, de galinha, inteiro, cru")
        public double Grams { get; private set; }

        // snapshot dos macros por 100g NO MOMENTO em que o alimento foi adicionado —
        // assim, se o dataset da TACO mudar depois, o plano salvo não muda de valor
        public double KcalPer100g { get; private set; }
        public double ProteinPer100g { get; private set; }
        public double FatPer100g { get; private set; }
        public double CarbohydratePer100g { get; private set; }
        public double FiberPer100g { get; private set; }

        public Guid MealId { get; private set; }
        public Meal Meal { get; private set; }

        private MealFoodItem() { } // exigido pelo EF Core

        public MealFoodItem(int tacoId, string description, double grams,
            double kcalPer100g, double proteinPer100g, double fatPer100g, double carbohydratePer100g, double fiberPer100g,
            Guid mealId)
        {
            TacoId = tacoId;
            Description = description;
            Grams = grams;
            KcalPer100g = kcalPer100g;
            ProteinPer100g = proteinPer100g;
            FatPer100g = fatPer100g;
            CarbohydratePer100g = carbohydratePer100g;
            FiberPer100g = fiberPer100g;
            MealId = mealId;
        }

        public void UpdateGrams(double grams)
        {
            Grams = grams;
        }
    }
}