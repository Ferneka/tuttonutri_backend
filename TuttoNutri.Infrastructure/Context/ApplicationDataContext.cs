using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TuttoNutri.Domain.Models;

namespace TuttoNutri.Infrastructure.Context
{
    public class ApplicationDataContext : IdentityDbContext<User>
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options){  }
        public DbSet<Nutritionist> Nutritionist {get; set;}
        // public DbSet<Address> Address {get; set;}
        // public DbSet<Patient> Patient {get; set;}
        public DbSet<Consultation> Consultation {get; set;}
        public DbSet<FoodPlan> FoodPlan {get; set;}
        public DbSet<User> User {get; set;}
        public DbSet<MedicalRecord> MedicalRecord {get; set;}
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }
        public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
        public DbSet<Meal> Meal {get; set;}
        public DbSet<MealFoodItem> MealFoodItem {get; set;}
        public DbSet<EmailChangeRequest> EmailChangeRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDataContext).Assembly);

            // modelBuilder.Entity<PatientProgress>()
            //     .HasOne(pp => pp.Patient)
            //     .WithMany(p => p.PatientProgress)
            //     .HasForeignKey(pp => pp.PatientId);

            base.OnModelCreating(modelBuilder);


        }
    }
}