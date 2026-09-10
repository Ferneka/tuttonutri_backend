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
                .IsRequired()
                .HasColumnType("timestamp without time zone");


            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(c => c.Nutritionist)
                .WithMany()
                .HasForeignKey(c => c.NutritionistId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
         
        }
    }
}