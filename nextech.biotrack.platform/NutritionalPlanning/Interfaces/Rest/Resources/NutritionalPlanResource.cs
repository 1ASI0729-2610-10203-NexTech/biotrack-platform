namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record NutritionalPlanResource(
    int Id,
    string Name,
    int CalorieTarget,
    int ProteinGrams,
    int CarbsGrams,
    int FatGrams,
    string Status,
    int NutritionistId,
    DateTime CreatedAt,
    DateTime UpdatedAt);
