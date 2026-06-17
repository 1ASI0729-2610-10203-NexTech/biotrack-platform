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
[SwaggerTag("Patient Weight Records endpoints")]
public class PatientWeightRecordsController(IWeightRecordCommandService commandService) : ControllerBase
{
    /// <summary>Update or create the patient's weight record for a given date</summary>
    [HttpPatch("{patientUserId:int}/weight")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Update patient weight", OperationId = "UpdateWeight")]
    [SwaggerResponse(StatusCodes.Status200OK, "Weight updated or created", typeof(WeightRecordResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid weight value")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Cannot update for another patient")]
    public async Task<IActionResult> UpdateWeight(
        int patientUserId,
        [FromBody] UpdateWeightResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return Problem("You can only update your own weight.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = UpdateWeightCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(this, result,
            record => Ok(WeightRecordResourceFromEntityAssembler.ToResourceFromEntity(record)));
    }
}
