using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProgressTrackingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ConsumptionRecord>().HasKey(r => r.Id);
        builder.Entity<ConsumptionRecord>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ConsumptionRecord>().Property(r => r.PatientUserId).IsRequired();
        builder.Entity<ConsumptionRecord>().Property(r => r.Date).IsRequired();
        builder.Entity<ConsumptionRecord>().Property(r => r.MealType).IsRequired().HasMaxLength(50);
        builder.Entity<ConsumptionRecord>().Property(r => r.Description).IsRequired().HasMaxLength(500);
        builder.Entity<ConsumptionRecord>().Property(r => r.Calories).IsRequired();

        builder.Entity<ActivityRecord>().HasKey(r => r.Id);
        builder.Entity<ActivityRecord>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ActivityRecord>().Property(r => r.PatientUserId).IsRequired();
        builder.Entity<ActivityRecord>().Property(r => r.Date).IsRequired();
        builder.Entity<ActivityRecord>().Property(r => r.ActivityType).IsRequired().HasMaxLength(50);
        builder.Entity<ActivityRecord>().Property(r => r.DurationMinutes).IsRequired();
        builder.Entity<ActivityRecord>().Property(r => r.Intensity).IsRequired().HasMaxLength(50);

        builder.Entity<WeightRecord>().HasKey(r => r.Id);
        builder.Entity<WeightRecord>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WeightRecord>().Property(r => r.PatientUserId).IsRequired();
        builder.Entity<WeightRecord>().Property(r => r.Date).IsRequired();
        builder.Entity<WeightRecord>().Property(r => r.WeightKg).IsRequired().HasPrecision(5, 2);
        builder.Entity<WeightRecord>().HasIndex(r => new { r.PatientUserId, r.Date }).IsUnique();

        builder.Entity<WeeklyAdherence>().HasKey(r => r.Id);
        builder.Entity<WeeklyAdherence>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WeeklyAdherence>().Property(r => r.PatientUserId).IsRequired();
        builder.Entity<WeeklyAdherence>().Property(r => r.PlanId).IsRequired();
        builder.Entity<WeeklyAdherence>().Property(r => r.WeekStart).IsRequired();
        builder.Entity<WeeklyAdherence>().Property(r => r.AdherencePct).IsRequired().HasPrecision(5, 2);

        builder.Entity<EvolutionReport>().HasKey(r => r.Id);
        builder.Entity<EvolutionReport>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<EvolutionReport>().Property(r => r.PatientUserId).IsRequired();
        builder.Entity<EvolutionReport>().Property(r => r.RequestedByUserId).IsRequired();
        builder.Entity<EvolutionReport>().Property(r => r.PeriodStart).IsRequired();
        builder.Entity<EvolutionReport>().Property(r => r.PeriodEnd).IsRequired();
        builder.Entity<EvolutionReport>().Property(r => r.Status).IsRequired().HasMaxLength(50);
        builder.Entity<EvolutionReport>().Property(r => r.PdfContent);
    }
}
