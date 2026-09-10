using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class PasswordResetCodeConfiguration : IEntityTypeConfiguration<PasswordResetCode>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PasswordResetCode> builder)
        {
            builder.ToTable("PasswordResetCodes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(p => p.ExpiresAt)
                .IsRequired();

            builder.Property(p => p.Used)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.UserId, p.Code });
        }
    }
}