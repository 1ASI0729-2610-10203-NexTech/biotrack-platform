using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class ProgressTrackingActionResultAssembler
{
    private static int ToStatusCode(ProgressTrackingError error) => error switch
    {
        ProgressTrackingError.RecordNotFound => StatusCodes.Status404NotFound,
        ProgressTrackingError.InvalidData => StatusCodes.Status400BadRequest,
        ProgressTrackingError.DatabaseError => StatusCodes.Status500InternalServerError,
        ProgressTrackingError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResult<T>(
        ControllerBase controller, Result<T> result, Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((ProgressTrackingError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
