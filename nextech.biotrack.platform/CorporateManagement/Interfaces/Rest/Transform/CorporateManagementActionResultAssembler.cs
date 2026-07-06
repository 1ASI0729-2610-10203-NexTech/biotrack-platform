using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.CorporateManagement.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;

public static class CorporateManagementActionResultAssembler
{
    private static int ToStatusCode(CorporateManagementError error) => error switch
    {
        CorporateManagementError.CompanyNotFound => StatusCodes.Status404NotFound,
        CorporateManagementError.RucAlreadyTaken => StatusCodes.Status409Conflict,
        CorporateManagementError.InvalidRucFormat => StatusCodes.Status400BadRequest,
        CorporateManagementError.AccessDenied => StatusCodes.Status403Forbidden,
        CorporateManagementError.InvalidCollaboratorData => StatusCodes.Status400BadRequest,
        CorporateManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
        CorporateManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResult(
        ControllerBase controller, Result result, Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();
        var statusCode = ToStatusCode((CorporateManagementError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller, Result<T> result, Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((CorporateManagementError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
