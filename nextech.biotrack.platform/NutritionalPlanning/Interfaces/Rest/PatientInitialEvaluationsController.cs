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
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Initial Evaluation endpoints")]
public class PatientInitialEvaluationsController(
    IInitialEvaluationCommandService commandService) : ControllerBase
{
    /// <summary>Complete the initial evaluation for a patient</summary>
    [HttpPost("{patientProfileId:int}/initial-evaluation")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Complete initial evaluation", OperationId = "CompleteInitialEvaluation")]
    [SwaggerResponse(StatusCodes.Status201Created, "Evaluation completed", typeof(InitialEvaluationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data (calories or macronutrients)")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Evaluation already exists for this patient")]
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
