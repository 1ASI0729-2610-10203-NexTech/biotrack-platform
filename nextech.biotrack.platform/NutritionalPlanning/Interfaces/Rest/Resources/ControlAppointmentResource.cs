namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record ControlAppointmentResource(
    int Id,
    int PatientUserId,
    int? NutritionistUserId,
    DateTime ScheduledAt,
    string Modality,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
