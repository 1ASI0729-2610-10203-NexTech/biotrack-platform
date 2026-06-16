using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Entities;
using HealthProfile = nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates.PatientProfile;

namespace nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPatientProfileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HealthProfile>().HasKey(p => p.Id);
        builder.Entity<HealthProfile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthProfile>().Property(p => p.PatientUserId).IsRequired();
        builder.Entity<HealthProfile>().HasIndex(p => p.PatientUserId).IsUnique();
        builder.Entity<HealthProfile>().Property(p => p.CurrentWeightKg).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Property(p => p.HeightCm).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Property(p => p.Age).IsRequired();
        builder.Entity<HealthProfile>().Property(p => p.BiologicalSex).IsRequired().HasMaxLength(20);
        builder.Entity<HealthProfile>().Property(p => p.ActivityLevel).IsRequired().HasMaxLength(20);
        builder.Entity<HealthProfile>().Property(p => p.SystolicPressure).IsRequired();
        builder.Entity<HealthProfile>().Property(p => p.DiastolicPressure).IsRequired();
        builder.Entity<HealthProfile>().Property(p => p.BasalGlucoseMgDl).IsRequired().HasPrecision(6, 2);
        builder.Entity<HealthProfile>().Property(p => p.Bmi).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Ignore(p => p.BmiCategory);
        builder.Entity<HealthProfile>().Property(p => p.NutritionalGoal).HasMaxLength(30);
        builder.Entity<HealthProfile>().Property(p => p.GoalStatus).IsRequired().HasMaxLength(20);
        builder.Entity<HealthProfile>().Property(p => p.IsComplete).IsRequired();

        builder.Entity<FoodRestriction>().HasKey(r => r.Id);
        builder.Entity<FoodRestriction>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<FoodRestriction>().Property(r => r.PatientProfileId).IsRequired();
        builder.Entity<FoodRestriction>().Property(r => r.Type).IsRequired().HasMaxLength(30);
        builder.Entity<FoodRestriction>().Property(r => r.Description).IsRequired().HasMaxLength(500);
        builder.Entity<FoodRestriction>().Property(r => r.Severity).IsRequired().HasMaxLength(20);

        builder.Entity<HealthProfile>()
            .HasMany(p => p.FoodRestrictions)
            .WithOne(r => r.PatientProfile)
            .HasForeignKey(r => r.PatientProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
