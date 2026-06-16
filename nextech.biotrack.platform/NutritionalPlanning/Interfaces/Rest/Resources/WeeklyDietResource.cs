namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record WeeklyDietResource(int PlanId, string Title, int Calories, IEnumerable<DayDietResource> Days);
