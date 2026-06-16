namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record InitialEvaluationResource(
    int Id,
    int PatientProfileId,
    int NutritionistUserId,
    string Observations,
    int CaloriesTarget,
    int ProteinsPct,
    int CarbohydratesPct,
    int FatsPct,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
