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
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
public class PatientInitialEvaluationsController(
    IInitialEvaluationCommandService commandService) : ControllerBase
{
    [HttpPost("{patientProfileId:int}/initial-evaluation")]
    public async Task<IActionResult> CompleteEvaluation(
        int patientProfileId,
        [FromBody] CompleteEvaluationResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = CompleteEvaluationCommandFromResourceAssembler
            .ToCommandFromResource(patientProfileId, currentUser.Id, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            evaluation => CreatedAtAction(nameof(CompleteEvaluation), new { patientProfileId },
                InitialEvaluationResourceFromEntityAssembler.ToResourceFromEntity(evaluation)));
    }
}
