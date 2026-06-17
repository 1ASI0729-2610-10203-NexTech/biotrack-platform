namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

public record AdherenceDataPoint(DateOnly WeekStart, decimal AdherencePct);
