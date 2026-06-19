using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProgressTrackingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<WeightRecord>().HasKey(r => r.Id);
        builder.Entity<WeightRecord>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WeightRecord>().Property(r => r.UserId).IsRequired();
        builder.Entity<WeightRecord>().Property(r => r.WeightKg).IsRequired().HasPrecision(5, 2);
        builder.Entity<WeightRecord>().Property(r => r.Notes).HasMaxLength(500);
        builder.Entity<WeightRecord>().Property(r => r.RecordedAt).IsRequired();
        builder.Entity<WeightRecord>().Property(r => r.CreatedAt).IsRequired();
        builder.Entity<WeightRecord>().Property(r => r.UpdatedAt).IsRequired();

        builder.Entity<FoodEntry>().HasKey(e => e.Id);
        builder.Entity<FoodEntry>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<FoodEntry>().Property(e => e.UserId).IsRequired();
        builder.Entity<FoodEntry>().Property(e => e.MealType).IsRequired().HasMaxLength(50);
        builder.Entity<FoodEntry>().Property(e => e.FoodName).IsRequired().HasMaxLength(200);
        builder.Entity<FoodEntry>().Property(e => e.Calories).IsRequired().HasPrecision(7, 2);
        builder.Entity<FoodEntry>().Property(e => e.LoggedAt).IsRequired();
        builder.Entity<FoodEntry>().Property(e => e.CreatedAt).IsRequired();
        builder.Entity<FoodEntry>().Property(e => e.UpdatedAt).IsRequired();

        builder.Entity<ActivityEntry>().HasKey(e => e.Id);
        builder.Entity<ActivityEntry>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ActivityEntry>().Property(e => e.UserId).IsRequired();
        builder.Entity<ActivityEntry>().Property(e => e.ActivityType).IsRequired().HasMaxLength(100);
        builder.Entity<ActivityEntry>().Property(e => e.DurationMinutes).IsRequired();
        builder.Entity<ActivityEntry>().Property(e => e.CaloriesBurned).IsRequired();
        builder.Entity<ActivityEntry>().Property(e => e.LoggedAt).IsRequired();
        builder.Entity<ActivityEntry>().Property(e => e.CreatedAt).IsRequired();
        builder.Entity<ActivityEntry>().Property(e => e.UpdatedAt).IsRequired();
    }
}
