namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record CalculateAdherenceCommand(int PatientUserId, int PlanId, DateOnly WeekStart);
