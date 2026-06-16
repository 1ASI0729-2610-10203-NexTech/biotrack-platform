using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/nutritional-plans")]
[Produces(MediaTypeNames.Application.Json)]
public class NutritionalPlansController(
    INutritionalPlanCommandService commandService) : ControllerBase
{
    [HttpPost]
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

    [HttpPatch("{planId:int}/accept")]
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

    [HttpPatch("{planId:int}/reject")]
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
