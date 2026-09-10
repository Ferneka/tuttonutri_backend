using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Phone)
                .HasMaxLength(20);

            builder.Property(p => p.Gender)
                .HasMaxLength(20);

            builder.Property(p => p.BirthOfDate)
                .IsRequired()
                .HasColumnType("date");
            
            builder.Property(p => p.Height)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            

            builder.HasOne(p => p.Nutritionist)
                .WithMany(n => n.Patients)  // agora referenciando a coleção
                .HasForeignKey(p => p.NutritionistId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

          

        }
    }
}