namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record PlanDayResponseResource(int Id, string DayOfWeek, IEnumerable<MealResponseResource> Meals);
