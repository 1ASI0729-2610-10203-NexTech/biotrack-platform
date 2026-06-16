using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/nutritional-plans")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Nutritional Plan endpoints")]
public class NutritionalPlansController(
    INutritionalPlanCommandService commandService) : ControllerBase
{
    /// <summary>Create a new nutritional plan for a patient (nutritionist only)</summary>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Create nutritional plan", OperationId = "CreateNutritionalPlan")]
    [SwaggerResponse(StatusCodes.Status201Created, "Plan created in Proposed status", typeof(NutritionalPlanResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid plan data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    public async Task<IActionResult> CreatePlan(
        [FromBody] CreateNutritionalPlanResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = CreatePlanCommandFromResourceAssembler.ToCommandFromResource(currentUser.Id, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            plan => CreatedAtAction(nameof(CreatePlan), new { planId = plan.Id },
                NutritionalPlanResourceFromEntityAssembler.ToResourceFromEntity(plan)));
    }

    /// <summary>Accept a proposed nutritional plan (patient only)</summary>
    [HttpPatch("{planId:int}/accept")]
    [SwaggerOperation(Summary = "Accept nutritional plan", OperationId = "AcceptNutritionalPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Plan accepted and activated", typeof(NutritionalPlanResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Plan is not in Proposed status")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Not the plan owner")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Plan not found")]
    public async Task<IActionResult> AcceptPlan(
        int planId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = AcceptPlanCommandFromResourceAssembler.ToCommandFromResource(planId, currentUser.Id);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            plan => Ok(NutritionalPlanResourceFromEntityAssembler.ToResourceFromEntity(plan)));
    }

    /// <summary>Reject a proposed nutritional plan (patient only)</summary>
    [HttpPatch("{planId:int}/reject")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Reject nutritional plan", OperationId = "RejectNutritionalPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Plan rejected", typeof(NutritionalPlanResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Plan is not in Proposed status")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Not the plan owner")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Plan not found")]
    public async Task<IActionResult> RejectPlan(
        int planId,
        [FromBody] RejectPlanResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = RejectPlanCommandFromResourceAssembler.ToCommandFromResource(planId, currentUser.Id, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            plan => Ok(NutritionalPlanResourceFromEntityAssembler.ToResourceFromEntity(plan)));
    }
}
