using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class NutritionistConfiguration : IEntityTypeConfiguration<Nutritionist>
    {
        public void Configure(EntityTypeBuilder<Nutritionist> builder)
        {
            builder.ToTable("Nutritionist");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Crn)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(n => n.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(n => n.PlanoAtivo)
                .HasMaxLength(20);

            builder.HasOne(n => n.User)
                .WithOne(u => u.Nutritionist)
                .HasForeignKey<Nutritionist>(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(n => n.Crn).IsUnique();
            // builder.HasMany(n => n.Patients)
            //     .WithOne(p => p.Nutritionist)
            //     .HasForeignKey(p => p.NutritionistId)
            //     .OnDelete(DeleteBehavior.NoAction);

            // builder.HasMany(n => n.Consultation)
            //     .WithOne(c => c.Nutritionist)
            //     .HasForeignKey(c => c.NutritionistId)
            //     .OnDelete(DeleteBehavior.NoAction);

            // builder.HasOne(n => n.Address)
            //     .WithOne()
            //     .HasForeignKey<Nutritionist>(n => n.AddressId)
            //     .IsRequired(false)
            //     .OnDelete(DeleteBehavior.NoAction);
        }
    }
}