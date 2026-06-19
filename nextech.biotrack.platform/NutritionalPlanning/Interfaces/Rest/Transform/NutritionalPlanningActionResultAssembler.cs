using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class NutritionalPlanningActionResultAssembler
{
    private static int ToStatusCode(NutritionalPlanningError error) => error switch
    {
        NutritionalPlanningError.PlanNotFound => StatusCodes.Status404NotFound,
        NutritionalPlanningError.InvalidPlanName => StatusCodes.Status400BadRequest,
        NutritionalPlanningError.InvalidCalorieTarget => StatusCodes.Status400BadRequest,
        NutritionalPlanningError.AccessDenied => StatusCodes.Status403Forbidden,
        NutritionalPlanningError.DatabaseError => StatusCodes.Status500InternalServerError,
        NutritionalPlanningError.InternalServerError => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };

    public static IActionResult ToActionResult<T>(
        ControllerBase controller, Result<T> result, Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((NutritionalPlanningError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
