using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    private static int ToStatusCode(IamError error)
    {
        return error switch
        {
            IamError.EmailAlreadyTaken => StatusCodes.Status409Conflict,
            IamError.InvalidCredentials => StatusCodes.Status400BadRequest,
            IamError.UserNotFound => StatusCodes.Status404NotFound,
            IamError.InvalidVerificationToken => StatusCodes.Status400BadRequest,
            IamError.EmailAlreadyVerified => StatusCodes.Status409Conflict,
            IamError.DatabaseError => StatusCodes.Status500InternalServerError,
            IamError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult(
        ControllerBase controller,
        Result result,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCode((IamError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((IamError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
