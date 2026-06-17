namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record AdherenceDataPointResource(DateOnly WeekStart, decimal AdherencePct);
