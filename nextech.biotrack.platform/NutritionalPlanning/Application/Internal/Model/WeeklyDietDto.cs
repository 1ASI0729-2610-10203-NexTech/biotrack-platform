namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.Model;

public record MealDto(string Type, string Name, string Description, int Calories);

public record DayDietDto(string DayOfWeek, int TotalCalories, IEnumerable<MealDto> Meals);

public record WeeklyDietDto(int PlanId, string Title, int Calories, IEnumerable<DayDietDto> Days);
