namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record CompleteEvaluationCommand(
    int PatientProfileId,
    int NutritionistUserId,
    string Observations,
    int CaloriesTarget,
    int ProteinsPct,
    int CarbohydratesPct,
    int FatsPct);
