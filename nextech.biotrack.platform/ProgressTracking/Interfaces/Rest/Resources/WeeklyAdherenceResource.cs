namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record WeeklyAdherenceResource(int Id, int PatientUserId, int PlanId, DateOnly WeekStart, decimal AdherencePct);
