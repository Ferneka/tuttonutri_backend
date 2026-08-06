using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecord");

            builder.HasKey(mr => mr.Id);

            builder.Property(mr => mr.ObservacoesClinicas)
                .HasMaxLength(2000);

            builder.Property(mr => mr.Weight)
                .HasColumnType("decimal(5,2)");

            builder.Property(mr => mr.Height)
                .HasColumnType("decimal(4,2)");

            builder.Property(mr => mr.BodyFat)
                .HasColumnType("decimal(4,2)");

            builder.Property(mr => mr.MuscleMass)
                .HasColumnType("decimal(5,2)");

            builder.Property(mr => mr.Objective)
                .HasMaxLength(200);

            builder.HasOne(mr => mr.Consultation)
                .WithOne(c => c.MedicalRecord)
                .HasForeignKey<MedicalRecord>(mr => mr.ConsultationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mr => mr.FoodPlan)
                .WithOne()
                .HasForeignKey<MedicalRecord>(mr => mr.FoodPlanId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}