using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient Adherence endpoints")]
public class PatientAdherenceController(IAdherenceCommandService commandService) : ControllerBase
{
    /// <summary>Calculate the weekly nutritional adherence for a patient</summary>
    [HttpPost("{patientUserId:int}/adherence/calculate")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Calculate weekly adherence", OperationId = "CalculateAdherence")]
    [SwaggerResponse(StatusCodes.Status200OK, "Adherence calculated", typeof(WeeklyAdherenceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Cannot calculate for another patient")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Insufficient data to calculate adherence")]
    public async Task<IActionResult> CalculateAdherence(
        int patientUserId,
        [FromBody] CalculateAdherenceResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId && currentUser.Role != "NUTRITIONIST")
            return Problem("You do not have permission to calculate adherence for this patient.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = CalculateAdherenceCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(this, result,
            adherence => Ok(WeeklyAdherenceResourceFromEntityAssembler.ToResourceFromEntity(adherence)));
    }
}
