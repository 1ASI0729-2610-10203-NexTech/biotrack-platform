namespace nextech.biotrack.platform.ProgressTracking.Domain.Model;

public enum ProgressTrackingError
{
    None,
    ConsumptionRecordNotFound,
    ActivityRecordNotFound,
    WeightRecordNotFound,
    WeeklyAdherenceNotFound,
    EvolutionReportNotFound,
    InvalidCalories,
    InvalidMealType,
    InvalidActivityType,
    InvalidActivityIntensity,
    InvalidDuration,
    InvalidWeight,
    InsufficientProgressData,
    AccessDenied,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
