using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Subscription Renewals endpoints")]
public class SubscriptionRenewalsController(IProcessRenewalCommandService renewalCommandService) : ControllerBase
{
    /// <summary>Process a subscription renewal</summary>
    [HttpPost("{subscriptionId:int}/renewal")]
    [SwaggerOperation(Summary = "Process subscription renewal", OperationId = "ProcessRenewal")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription renewed successfully", typeof(SubscriptionResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status402PaymentRequired, "Renewal payment was rejected")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription not found")]
    public async Task<IActionResult> ProcessRenewal(
        int subscriptionId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new ProcessRenewalCommand(subscriptionId, currentUser.Id);
        var result = await renewalCommandService.Handle(command, cancellationToken);
        return SubscriptionsBillingActionResultAssembler.ToActionResult(this, result,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)));
    }
}
