namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record MealInputDto(string Type, string Name, string Description, int Calories);

public record PlanDayInputDto(string DayOfWeek, IEnumerable<MealInputDto> Meals);

public record CreatePlanCommand(
    int PatientProfileId,
    int PatientUserId,
    int NutritionistUserId,
    string Title,
    string Description,
    int PlanDurationDays,
    IEnumerable<PlanDayInputDto> Days);
