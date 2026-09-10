using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class FoodPlanConfiguration : IEntityTypeConfiguration<FoodPlan>
    {
        public void Configure(EntityTypeBuilder<FoodPlan> builder)
        {
            builder.ToTable("FoodPlan");

            builder.HasKey(fp => fp.Id);

            builder.Property(fp => fp.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(fp => fp.Calories)
                .IsRequired()
                .HasColumnType("decimal(7,2)");

            builder.Property(fp => fp.Protein)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(fp => fp.Carbohydrate)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(fp => fp.Fat)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(fp => fp.Fiber) // NOVO
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(fp => fp.Observations)
                .HasMaxLength(500);

            builder.Property(fp => fp.InitDate)
                .IsRequired()
                .HasColumnType("timestamp without time zone"); // NOVO — evita o erro de Kind=Unspecified do Npgsql

            builder.Property(fp => fp.EndDate)
                .HasColumnType("timestamp without time zone"); // NOVO

            builder.Property(fp => fp.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // NOVO: FoodPlan (1) -> Meal (N), com backing field porque Meals é exposto como IReadOnlyCollection
            builder.HasMany(fp => fp.Meals)
                .WithOne(m => m.FoodPlan)
                .HasForeignKey(m => m.FoodPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(fp => fp.Meals)
                .HasField("_meals")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // builder.HasOne(fp => fp.Patient)
            //     .WithOne(p => p.FoodPlan)
            //     .HasForeignKey<FoodPlan>(fp => fp.PatientId)
            //     .OnDelete(DeleteBehavior.Cascade); 
            
            // builder.HasOne(fp => fp.Nutritionist)
            //     .WithMany(n => n.FoodPlan)
            //     .HasForeignKey(fp => fp.NutritionistId)
            //     .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}