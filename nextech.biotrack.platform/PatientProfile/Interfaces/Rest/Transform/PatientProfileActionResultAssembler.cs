using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.PatientProfile.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class PatientProfileActionResultAssembler
{
    private static int ToStatusCode(PatientProfileError error) => error switch
    {
        PatientProfileError.ProfileNotFound => StatusCodes.Status404NotFound,
        PatientProfileError.ProfileAlreadyExists => StatusCodes.Status409Conflict,
        PatientProfileError.InvalidHealthData => StatusCodes.Status400BadRequest,
        PatientProfileError.AccessDenied => StatusCodes.Status403Forbidden,
        PatientProfileError.InvalidGoal => StatusCodes.Status400BadRequest,
        PatientProfileError.InvalidRestrictionData => StatusCodes.Status400BadRequest,
        PatientProfileError.DatabaseError => StatusCodes.Status500InternalServerError,
        PatientProfileError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResult(
        ControllerBase controller, Result result, Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();
        var statusCode = ToStatusCode((PatientProfileError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller, Result<T> result, Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((PatientProfileError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
