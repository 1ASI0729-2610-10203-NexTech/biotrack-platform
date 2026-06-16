namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.Model;

public record InitialEvaluationDto(
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
