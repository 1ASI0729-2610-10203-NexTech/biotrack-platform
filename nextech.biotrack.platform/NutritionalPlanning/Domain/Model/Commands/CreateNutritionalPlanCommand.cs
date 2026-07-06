namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record CreateNutritionalPlanCommand(
    string Name,
    int CalorieTarget,
    int ProteinGrams,
    int CarbsGrams,
    int FatGrams,
    int NutritionistId);
