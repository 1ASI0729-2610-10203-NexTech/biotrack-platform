using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Errors;

public static class ProgressTrackingErrors
{
    public static readonly Error ConsumptionRecordNotFound =
        new("ProgressTracking.ConsumptionRecordNotFound", "The specified consumption record was not found.");

    public static readonly Error ActivityRecordNotFound =
        new("ProgressTracking.ActivityRecordNotFound", "The specified activity record was not found.");

    public static readonly Error WeightRecordNotFound =
        new("ProgressTracking.WeightRecordNotFound", "The specified weight record was not found.");

    public static readonly Error WeeklyAdherenceNotFound =
        new("ProgressTracking.WeeklyAdherenceNotFound", "The specified weekly adherence record was not found.");

    public static readonly Error EvolutionReportNotFound =
        new("ProgressTracking.EvolutionReportNotFound", "The specified evolution report was not found.");

    public static readonly Error InvalidCalories =
        new("ProgressTracking.InvalidCalories", "Calories must be greater than 0.");

    public static readonly Error InvalidMealType =
        new("ProgressTracking.InvalidMealType", "The specified meal type is not valid.");

    public static readonly Error InvalidActivityType =
        new("ProgressTracking.InvalidActivityType", "The specified activity type is not valid.");

    public static readonly Error InvalidActivityIntensity =
        new("ProgressTracking.InvalidActivityIntensity", "The specified activity intensity is not valid.");

    public static readonly Error InvalidDuration =
        new("ProgressTracking.InvalidDuration", "Duration must be greater than 0.");

    public static readonly Error InvalidWeight =
        new("ProgressTracking.InvalidWeight", "Weight must be greater than 0.");

    public static readonly Error InsufficientProgressData =
        new("ProgressTracking.InsufficientProgressData", "Not enough data is available to complete this operation.");

    public static readonly Error AccessDenied =
        new("ProgressTracking.AccessDenied", "You do not have permission to access this resource.");

    public static readonly Error DatabaseError =
        new("ProgressTracking.DatabaseError", "A database error occurred.");

    public static readonly Error InternalServerError =
        new("ProgressTracking.InternalServerError", "An unexpected error occurred.");
}
