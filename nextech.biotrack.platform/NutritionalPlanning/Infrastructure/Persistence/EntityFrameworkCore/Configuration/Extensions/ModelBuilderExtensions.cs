using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyNutritionalPlanningConfiguration(this ModelBuilder builder)
    {
        builder.Entity<NutritionalPlan>().HasKey(p => p.Id);
        builder.Entity<NutritionalPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<NutritionalPlan>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Entity<NutritionalPlan>().Property(p => p.CalorieTarget).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.ProteinGrams).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.CarbsGrams).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.FatGrams).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.NutritionistId).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.Status).IsRequired().HasMaxLength(20);
        builder.Entity<NutritionalPlan>().Property(p => p.CreatedAt).IsRequired();
        builder.Entity<NutritionalPlan>().Property(p => p.UpdatedAt).IsRequired();
    }
}
