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
[SwaggerTag("Patient Food Logs endpoints")]
public class PatientFoodLogsController(IConsumptionRecordCommandService commandService) : ControllerBase
{
    /// <summary>Register a food consumption entry for a patient</summary>
    [HttpPost("{patientUserId:int}/food-log")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Register food consumption", OperationId = "RegisterConsumption")]
    [SwaggerResponse(StatusCodes.Status201Created, "Consumption registered", typeof(ConsumptionRecordResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid meal type or calories")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Cannot register for another patient")]
    public async Task<IActionResult> RegisterConsumption(
        int patientUserId,
        [FromBody] RegisterConsumptionResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return Problem("You can only register your own food consumption.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = RegisterConsumptionCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(this, result,
            record => CreatedAtAction(nameof(RegisterConsumption), new { patientUserId },
                ConsumptionRecordResourceFromEntityAssembler.ToResourceFromEntity(record)));
    }
}
