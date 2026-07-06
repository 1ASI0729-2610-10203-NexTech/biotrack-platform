using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Subscriptions endpoints")]
public class SubscriptionsController(
    IActivateSubscriptionCommandService activateCommandService,
    ISuspendSubscriptionCommandService suspendCommandService,
    IReactivateSubscriptionCommandService reactivateCommandService)
    : ControllerBase
{
    /// <summary>Activate a new subscription for the authenticated user</summary>
    [HttpPost("activate")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Activate subscription", OperationId = "ActivateSubscription")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription activated successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status402PaymentRequired, "Payment was rejected")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription plan not found")]
    public async Task<IActionResult> ActivateSubscription(
        [FromBody] ActivateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = ActivateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(currentUser.Id, resource);
        var result = await activateCommandService.Handle(command, cancellationToken);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }

    /// <summary>Suspend a subscription</summary>
    [HttpPatch("{subscriptionId:int}/suspend")]
    [SwaggerOperation(Summary = "Suspend subscription", OperationId = "SuspendSubscription")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription suspended successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription not found")]
    public async Task<IActionResult> SuspendSubscription(
        int subscriptionId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = SuspendSubscriptionCommandFromResourceAssembler.ToCommandFromResource(subscriptionId, currentUser.Id);
        var result = await suspendCommandService.Handle(command, cancellationToken);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }

    /// <summary>Reactivate a suspended or pending subscription</summary>
    [HttpPatch("{subscriptionId:int}/reactivate")]
    [SwaggerOperation(Summary = "Reactivate subscription", OperationId = "ReactivateSubscription")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription reactivated successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Subscription not eligible for reactivation")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status402PaymentRequired, "Payment was rejected")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription not found")]
    public async Task<IActionResult> ReactivateSubscription(
        int subscriptionId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = ReactivateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(subscriptionId, currentUser.Id);
        var result = await reactivateCommandService.Handle(command, cancellationToken);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }
}
