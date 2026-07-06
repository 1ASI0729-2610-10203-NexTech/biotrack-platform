using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class NutritionalPlanResourceFromEntityAssembler
{
    public static NutritionalPlanResource ToResourceFromEntity(NutritionalPlan plan) =>
        new(plan.Id, plan.Name, plan.CalorieTarget, plan.ProteinGrams,
            plan.CarbsGrams, plan.FatGrams, plan.Status, plan.NutritionistId,
            plan.CreatedAt, plan.UpdatedAt);
}
