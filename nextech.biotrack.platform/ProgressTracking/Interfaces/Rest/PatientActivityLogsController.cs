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
[SwaggerTag("Patient Activity Logs endpoints")]
public class PatientActivityLogsController(IActivityRecordCommandService commandService) : ControllerBase
{
    /// <summary>Register a physical activity entry for a patient</summary>
    [HttpPost("{patientUserId:int}/activity-log")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Register physical activity", OperationId = "RegisterActivity")]
    [SwaggerResponse(StatusCodes.Status201Created, "Activity registered", typeof(ActivityRecordResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid activity type, intensity or duration")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Cannot register for another patient")]
    public async Task<IActionResult> RegisterActivity(
        int patientUserId,
        [FromBody] RegisterActivityResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return Problem("You can only register your own activity.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = RegisterActivityCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(this, result,
            record => CreatedAtAction(nameof(RegisterActivity), new { patientUserId },
                ActivityRecordResourceFromEntityAssembler.ToResourceFromEntity(record)));
    }
}
