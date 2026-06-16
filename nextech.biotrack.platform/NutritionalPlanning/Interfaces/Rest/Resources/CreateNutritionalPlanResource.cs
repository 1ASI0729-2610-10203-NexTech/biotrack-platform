namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record CreateNutritionalPlanResource(
    int PatientProfileId,
    int PatientUserId,
    string Title,
    string Description,
    int PlanDurationDays,
    IEnumerable<PlanDayResource> Days);
