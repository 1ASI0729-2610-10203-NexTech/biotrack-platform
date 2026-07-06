using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.SubscriptionsBilling.Application.QueryServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Queries;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Billing Summary endpoints")]
public class BillingSummaryController(IBillingSummaryQueryService queryService) : ControllerBase
{
    /// <summary>Get the billing summary for a subscription</summary>
    [HttpGet("{subscriptionId:int}/billing-summary")]
    [SwaggerOperation(Summary = "Get billing summary", OperationId = "GetBillingSummary")]
    [SwaggerResponse(StatusCodes.Status200OK, "Billing summary returned", typeof(BillingSummaryResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subscription not found")]
    public async Task<IActionResult> GetBillingSummary(
        int subscriptionId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var query = new GetBillingSummaryQuery(subscriptionId, currentUser.Id);
        var result = await queryService.Handle(query, cancellationToken);
        if (result == null)
            return Problem("Subscription not found or access denied.", statusCode: StatusCodes.Status404NotFound);

        return Ok(BillingSummaryResourceFromResultAssembler.ToResourceFromResult(result));
    }
}
