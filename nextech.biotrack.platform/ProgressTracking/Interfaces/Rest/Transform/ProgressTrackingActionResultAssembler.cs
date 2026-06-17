using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class ProgressTrackingActionResultAssembler
{
    private static int ToStatusCode(ProgressTrackingError error)
    {
        return error switch
        {
            ProgressTrackingError.ConsumptionRecordNotFound => StatusCodes.Status404NotFound,
            ProgressTrackingError.ActivityRecordNotFound => StatusCodes.Status404NotFound,
            ProgressTrackingError.WeightRecordNotFound => StatusCodes.Status404NotFound,
            ProgressTrackingError.WeeklyAdherenceNotFound => StatusCodes.Status404NotFound,
            ProgressTrackingError.EvolutionReportNotFound => StatusCodes.Status404NotFound,
            ProgressTrackingError.InvalidCalories => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InvalidMealType => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InvalidActivityType => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InvalidActivityIntensity => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InvalidDuration => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InvalidWeight => StatusCodes.Status400BadRequest,
            ProgressTrackingError.InsufficientProgressData => StatusCodes.Status422UnprocessableEntity,
            ProgressTrackingError.AccessDenied => StatusCodes.Status403Forbidden,
            ProgressTrackingError.DatabaseError => StatusCodes.Status500InternalServerError,
            ProgressTrackingError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult(
        ControllerBase controller,
        Result result,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();
        var statusCode = ToStatusCode((ProgressTrackingError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((ProgressTrackingError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
