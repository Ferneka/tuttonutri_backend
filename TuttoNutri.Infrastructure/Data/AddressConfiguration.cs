using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);
 
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);
 
            builder.Property(p => p.Addresses)
                .IsRequired()
                .HasMaxLength(255);
 
            builder.Property(p => p.Number)
                .IsRequired();
 
            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(255);
 
            builder.Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(10);
 
            builder.Property(p => p.Country)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}