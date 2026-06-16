namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record NutritionalPlanResource(
    int Id,
    int PatientProfileId,
    int PatientUserId,
    int NutritionistUserId,
    string Title,
    string Description,
    int PlanDurationDays,
    string Status,
    string? RejectionNotes,
    IEnumerable<PlanDayResponseResource> Days,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
