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
    /// <summary>Create and activate a new subscription for the authenticated user</summary>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Create subscription", OperationId = "CreateSubscription")]
    [SwaggerResponse(StatusCodes.Status201Created, "Subscription created successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status402PaymentRequired, "Payment was rejected")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription plan not found")]
    public async Task<IActionResult> CreateSubscription(
        [FromBody] ActivateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = ActivateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(currentUser.Id, resource);
        var result = await activateCommandService.Handle(command, cancellationToken);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => StatusCode(StatusCodes.Status201Created,
                SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }

    /// <summary>Update subscription status (Suspended or Active)</summary>
    [HttpPatch("{subscriptionId:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Update subscription status", OperationId = "UpdateSubscriptionStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription updated successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid status. Use 'Suspended' or 'Active'")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status402PaymentRequired, "Payment was rejected")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription not found")]
    public async Task<IActionResult> UpdateSubscriptionStatus(
        int subscriptionId,
        [FromBody] UpdateSubscriptionStatusResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;

        return resource.Status switch
        {
            "Suspended" => await HandleSuspend(subscriptionId, currentUser.Id, cancellationToken),
            "Active" => await HandleReactivate(subscriptionId, currentUser.Id, cancellationToken),
            _ => BadRequest(new { message = $"Invalid status '{resource.Status}'. Use 'Suspended' or 'Active'." })
        };
    }

    private async Task<IActionResult> HandleSuspend(int subscriptionId, int userId, CancellationToken ct)
    {
        var command = SuspendSubscriptionCommandFromResourceAssembler.ToCommandFromResource(subscriptionId, userId);
        var result = await suspendCommandService.Handle(command, ct);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }

    private async Task<IActionResult> HandleReactivate(int subscriptionId, int userId, CancellationToken ct)
    {
        var command = ReactivateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(subscriptionId, userId);
        var result = await reactivateCommandService.Handle(command, ct);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }
}
