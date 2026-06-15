using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Errors;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    private static int ToStatusCode(IamErrors error)
    {
        return error switch
        {
            IamErrors.EmailAlreadyTaken => StatusCodes.Status409Conflict,
            IamErrors.InvalidCredentials => StatusCodes.Status400BadRequest,
            IamErrors.UserNotFound => StatusCodes.Status404NotFound,
            IamErrors.InvalidVerificationToken => StatusCodes.Status400BadRequest,
            IamErrors.EmailAlreadyVerified => StatusCodes.Status409Conflict,
            IamErrors.DatabaseError => StatusCodes.Status500InternalServerError,
            IamErrors.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult(
        ControllerBase controller,
        Result result,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCode((IamErrors)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((IamErrors)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
