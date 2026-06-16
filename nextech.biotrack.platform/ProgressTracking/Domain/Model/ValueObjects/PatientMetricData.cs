namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

public record PatientMetricData(
    int PatientUserId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    decimal AverageCalories,
    decimal AverageWeightKg,
    decimal AverageAdherencePct,
    int TotalActivityMinutes);
