using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Entities;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyNutritionalPlanningConfiguration(this ModelBuilder builder)
    {
        builder.Entity<InitialEvaluation>().HasKey(e => e.Id);
        builder.Entity<InitialEvaluation>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<InitialEvaluation>().Property(e => e.PatientProfileId).IsRequired();
        builder.Entity<InitialEvaluation>().HasIndex(e => e.PatientProfileId).IsUnique();
        builder.Entity<InitialEvaluation>().Property(e => e.NutritionistUserId).IsRequired();
        builder.Entity<InitialEvaluation>().Property(e => e.Observations).IsRequired().HasMaxLength(2000);
        builder.Entity<InitialEvaluation>().Property(e => e.CaloriesTarget).IsRequired();
        builder.Entity<InitialEvaluation>().Property(e => e.ProteinsPct).IsRequired();
        builder.Entity<InitialEvaluation>().Property(e => e.CarbohydratesPct).IsRequired();
        builder.Entity<InitialEvaluation>().Property(e => e.FatsPct).IsRequired();
        builder.Entity<InitialEvaluation>().Property(e => e.Status).IsRequired().HasMaxLength(50);

        builder.Entity<NutritionalPlan>().HasKey(p => p.Id);
        builder.Entity<NutritionalPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NutritionalPlan>().Property(p => p.PatientProfileId).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.PatientUserId).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.NutritionistUserId).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Entity<NutritionalPlan>().Property(p => p.Description).HasMaxLength(2000);
        builder.Entity<NutritionalPlan>().Property(p => p.PlanDurationDays).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.Status).IsRequired().HasMaxLength(50);
        builder.Entity<NutritionalPlan>().Property(p => p.RejectionNotes).HasMaxLength(1000);
        builder.Entity<NutritionalPlan>().Ignore(p => p.IsProposed);

        builder.Entity<PlanDay>().HasKey(d => d.Id);
        builder.Entity<PlanDay>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PlanDay>().Property(d => d.DayOfWeek).IsRequired().HasMaxLength(20);
        builder.Entity<NutritionalPlan>()
            .HasMany(p => p.Days)
            .WithOne(d => d.NutritionalPlan)
            .HasForeignKey(d => d.NutritionalPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Meal>().HasKey(m => m.Id);
        builder.Entity<Meal>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Meal>().Property(m => m.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Meal>().Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Meal>().Property(m => m.Description).HasMaxLength(500);
        builder.Entity<Meal>().Property(m => m.Calories).IsRequired();
        builder.Entity<PlanDay>()
            .HasMany(d => d.Meals)
            .WithOne(m => m.PlanDay)
            .HasForeignKey(m => m.PlanDayId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ControlAppointment>().HasKey(a => a.Id);
        builder.Entity<ControlAppointment>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ControlAppointment>().Property(a => a.PatientUserId).IsRequired();
        builder.Entity<ControlAppointment>().Property(a => a.NutritionistUserId);
        builder.Entity<ControlAppointment>().Property(a => a.ScheduledAt).IsRequired();
        builder.Entity<ControlAppointment>().Property(a => a.Modality).IsRequired().HasMaxLength(50);
        builder.Entity<ControlAppointment>().Property(a => a.Status).IsRequired().HasMaxLength(50);
    }
}
