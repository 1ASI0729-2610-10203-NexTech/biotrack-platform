namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

public record ProgressChartResult(
    int PatientUserId,
    IEnumerable<WeightDataPoint> WeightHistory,
    IEnumerable<AdherenceDataPoint> AdherenceHistory,
    decimal OverallChangeWeight,
    decimal? TargetWeight,
    decimal AverageAdherencePct);
