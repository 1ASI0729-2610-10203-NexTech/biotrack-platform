namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record ProgressChartResource(
    int PatientUserId,
    IEnumerable<WeightDataPointResource> WeightHistory,
    IEnumerable<AdherenceDataPointResource> AdherenceHistory,
    decimal OverallChangeWeight,
    decimal? TargetWeight,
    decimal AverageAdherencePct);
