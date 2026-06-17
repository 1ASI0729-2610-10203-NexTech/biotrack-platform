namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record EvolutionReportResource(int Id, int PatientUserId, DateOnly PeriodStart, DateOnly PeriodEnd, string Status);
