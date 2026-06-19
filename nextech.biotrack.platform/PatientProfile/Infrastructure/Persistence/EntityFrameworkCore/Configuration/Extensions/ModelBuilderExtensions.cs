using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPatientProfileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HealthProfile>().HasKey(p => p.Id);
        builder.Entity<HealthProfile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HealthProfile>().Property(p => p.UserId).IsRequired();
        builder.Entity<HealthProfile>().HasIndex(p => p.UserId).IsUnique();
        builder.Entity<HealthProfile>().Property(p => p.HeightCm).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Property(p => p.WeightKg).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Property(p => p.GoalWeightKg).IsRequired().HasPrecision(5, 2);
        builder.Entity<HealthProfile>().Property(p => p.ActivityLevel).IsRequired().HasMaxLength(50);
        builder.Entity<HealthProfile>().Property(p => p.NutritionalObjective).IsRequired().HasMaxLength(200);
        builder.Entity<HealthProfile>().Property(p => p.DietaryRestrictions).HasMaxLength(500);
        builder.Entity<HealthProfile>().Property(p => p.CreatedAt).IsRequired();
        builder.Entity<HealthProfile>().Property(p => p.UpdatedAt).IsRequired();
        builder.Entity<HealthProfile>().Ignore(p => p.Bmi);
    }
}
