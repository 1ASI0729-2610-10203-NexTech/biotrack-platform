using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class NutritionalPlanResourceFromEntityAssembler
{
    public static NutritionalPlanResource ToResourceFromEntity(NutritionalPlan plan) =>
        new(plan.Id, plan.PatientProfileId, plan.PatientUserId, plan.NutritionistUserId,
            plan.Title, plan.Description, plan.PlanDurationDays, plan.Status, plan.RejectionNotes,
            plan.Days.Select(d => new PlanDayResponseResource(
                d.Id, d.DayOfWeek,
                d.Meals.Select(m => new MealResponseResource(m.Type, m.Name, m.Description, m.Calories)))),
            plan.CreatedAt, plan.UpdatedAt);
}
