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

            builder.Property(mr => mr.EvaluationDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(mr => mr.ReferenciaComposicao)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(mr => mr.ObservacoesClinicas)
                .HasMaxLength(2000);

            builder.Property(mr => mr.Weight).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.Height).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.BodyFat).HasColumnType("decimal(4,2)");
            builder.Property(mr => mr.MuscleMass).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.Objective).HasMaxLength(200);

            // Circunferências
            builder.Property(mr => mr.CircPescoco).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircTorax).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircCintura).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircAbdomen).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircQuadril).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircBracoRelaxado).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircBracoContraido).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircAntebraco).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircCoxaProximal).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.CircPanturrilha).HasColumnType("decimal(5,2)");

            // Dobras cutâneas
            builder.Property(mr => mr.DobraTriceps).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraSubescapular).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraAxilarMedia).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraPeitoral).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraSupraIliaca).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraAbdominal).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DobraCoxa).HasColumnType("decimal(5,2)");

            // Diâmetros ósseos
            builder.Property(mr => mr.DiamPunho).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DiamFemur).HasColumnType("decimal(5,2)");
            builder.Property(mr => mr.DiamUmero).HasColumnType("decimal(5,2)");

            builder.HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mr => mr.Nutritionist)
                .WithMany()
                .HasForeignKey(mr => mr.NutritionistId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mr => mr.Consultation)
                .WithOne()
                .HasForeignKey<MedicalRecord>(mr => mr.ConsultationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(mr => mr.FoodPlan)
                .WithOne()
                .HasForeignKey<MedicalRecord>(mr => mr.FoodPlanId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}