using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class EmailChangeRequestConfiguration : IEntityTypeConfiguration<EmailChangeRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EmailChangeRequest> builder)
        {
           
            builder.ToTable("EmailChangeRequest");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.NewEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(e => e.UserId)
                .IsRequired();
        }
    }
}