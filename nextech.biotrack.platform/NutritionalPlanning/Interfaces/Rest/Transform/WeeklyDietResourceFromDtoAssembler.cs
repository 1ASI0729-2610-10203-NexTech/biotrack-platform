using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.Model;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class WeeklyDietResourceFromDtoAssembler
{
    public static WeeklyDietResource ToResourceFromDto(WeeklyDietDto dto) =>
        new(dto.PlanId, dto.Title, dto.Calories,
            dto.Days.Select(d => new DayDietResource(
                d.DayOfWeek, d.TotalCalories,
                d.Meals.Select(m => new MealDietResource(m.Type, m.Name, m.Description, m.Calories)))));
}
