using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class SubscriptionsBillingActionResultAssembler
{
    private static int ToStatusCode(SubscriptionsBillingError error)
    {
        return error switch
        {
            SubscriptionsBillingError.SubscriptionNotFound => StatusCodes.Status404NotFound,
            SubscriptionsBillingError.PlanNotFound => StatusCodes.Status404NotFound,
            SubscriptionsBillingError.PaymentFailed => StatusCodes.Status402PaymentRequired,
            SubscriptionsBillingError.PaymentRejected => StatusCodes.Status402PaymentRequired,
            SubscriptionsBillingError.SubscriptionAlreadyActive => StatusCodes.Status400BadRequest,
            SubscriptionsBillingError.SubscriptionNotSuspended => StatusCodes.Status400BadRequest,
            SubscriptionsBillingError.SubscriptionNotEligibleForReactivation => StatusCodes.Status400BadRequest,
            SubscriptionsBillingError.AccessDenied => StatusCodes.Status403Forbidden,
            SubscriptionsBillingError.InvalidStartDate => StatusCodes.Status400BadRequest,
            SubscriptionsBillingError.InvalidSubscriptionId => StatusCodes.Status400BadRequest,
            SubscriptionsBillingError.DatabaseError => StatusCodes.Status500InternalServerError,
            SubscriptionsBillingError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResult(
        ControllerBase controller,
        Result result,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();
        var statusCode = ToStatusCode((SubscriptionsBillingError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }

    public static IActionResult ToActionResult<T>(
        ControllerBase controller,
        Result<T> result,
        Func<T, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = ToStatusCode((SubscriptionsBillingError)result.Error!);
        return controller.Problem(result.Message, statusCode: statusCode);
    }
}
