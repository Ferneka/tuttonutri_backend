using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Data
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.ToTable("Meal");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Time); // TimeSpan?, opcional

            builder.Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Meal (1) -> MealFoodItem (N)
            builder.HasMany(m => m.Items)
                .WithOne(i => i.Meal)
                .HasForeignKey(i => i.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(m => m.Items)
                .HasField("_items")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}