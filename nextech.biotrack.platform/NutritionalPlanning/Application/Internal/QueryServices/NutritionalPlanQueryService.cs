using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.Model;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.QueryServices;

public class NutritionalPlanQueryService(INutritionalPlanRepository planRepository) : INutritionalPlanQueryService
{
    public async Task<WeeklyDietDto?> Handle(GetWeeklyDietQuery query, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindActivePlanByPatientUserIdAsync(query.PatientUserId, cancellationToken);
        if (plan == null) return null;

        var totalCalories = plan.Days.SelectMany(d => d.Meals).Sum(m => m.Calories);

        var days = plan.Days.Select(d => new DayDietDto(
            d.DayOfWeek,
            d.Meals.Sum(m => m.Calories),
            d.Meals.Select(m => new MealDto(m.Type, m.Name, m.Description, m.Calories))));

        return new WeeklyDietDto(plan.Id, plan.Title, totalCalories, days);
    }
}
