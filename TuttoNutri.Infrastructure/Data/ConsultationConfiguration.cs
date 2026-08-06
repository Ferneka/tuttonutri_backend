using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.ToTable("Consultation");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Observations)
                .HasMaxLength(500);

            builder.Property(c => c.MinuteDuration)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(c => c.Patient)
                .WithMany(p => p.Consultation)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(c => c.Nutritionist)
            //     .WithMany(n => n.Consultation)
            //     .HasForeignKey(c => c.NutritionistId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}