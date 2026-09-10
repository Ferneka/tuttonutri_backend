using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class MealFoodItemConfiguration : IEntityTypeConfiguration<MealFoodItem>
    {
        public void Configure(EntityTypeBuilder<MealFoodItem> builder)
        {
            builder.ToTable("MealFoodItem");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.TacoId)
                .IsRequired();

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.Grams)
                .IsRequired()
                .HasColumnType("decimal(7,2)");

            builder.Property(i => i.KcalPer100g)
                .IsRequired()
                .HasColumnType("decimal(7,2)");

            builder.Property(i => i.ProteinPer100g)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(i => i.FatPer100g)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(i => i.CarbohydratePer100g)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(i => i.FiberPer100g)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(i => i.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}