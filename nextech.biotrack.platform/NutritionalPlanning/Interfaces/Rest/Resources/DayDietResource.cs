namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record DayDietResource(string DayOfWeek, int TotalCalories, IEnumerable<MealDietResource> Meals);
