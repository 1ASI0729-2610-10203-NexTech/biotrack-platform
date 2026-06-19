namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record CreateNutritionalPlanResource(
    string Name,
    int CalorieTarget,
    int ProteinGrams,
    int CarbsGrams,
    int FatGrams);
