namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record PlanDayResource(string DayOfWeek, IEnumerable<MealResource> Meals);
