namespace nextech.biotrack.platform.ProgressTracking.Application.Dtos;

public record ProgressChartDto(
    int PatientUserId,
    IEnumerable<WeightDataPoint> WeightHistory,
    IEnumerable<AdherenceDataPoint> AdherenceHistory,
    decimal OverallChangeWeight,
    decimal? TargetWeight,
    decimal AverageAdherencePct);
