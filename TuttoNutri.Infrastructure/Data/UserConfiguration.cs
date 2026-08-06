using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            // builder.Property(u => u.Gender)
            //     .IsRequired()
            //     .HasMaxLength(20);

            // builder.Property(u => u.BirthOfDate)
            //     .IsRequired();

            builder.Property(u => u.Profile)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(u => u.Nutritionist)
                .WithOne(n => n.User)
                .HasForeignKey<Nutritionist>(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

          
        }
    }
}