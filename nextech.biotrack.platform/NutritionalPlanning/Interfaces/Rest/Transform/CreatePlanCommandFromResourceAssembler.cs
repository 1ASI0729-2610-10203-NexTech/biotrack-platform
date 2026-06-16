using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(int nutritionistUserId, CreateNutritionalPlanResource resource) =>
        new(resource.PatientProfileId, resource.PatientUserId, nutritionistUserId,
            resource.Title, resource.Description, resource.PlanDurationDays,
            resource.Days.Select(d => new PlanDayInputDto(
                d.DayOfWeek,
                d.Meals.Select(m => new MealInputDto(m.Type, m.Name, m.Description, m.Calories)))));
}
