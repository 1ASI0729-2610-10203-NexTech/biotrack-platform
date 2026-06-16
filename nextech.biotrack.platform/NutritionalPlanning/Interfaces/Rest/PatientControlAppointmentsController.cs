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
[SwaggerTag("Patient Control Appointments endpoints")]
public class PatientControlAppointmentsController(
    IControlAppointmentCommandService commandService) : ControllerBase
{
    /// <summary>Schedule a control appointment for a patient</summary>
    [HttpPost("{patientUserId:int}/control-appointments")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Schedule control appointment", OperationId = "ScheduleControlAppointment")]
    [SwaggerResponse(StatusCodes.Status201Created, "Appointment scheduled", typeof(ControlAppointmentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid date or modality")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Cannot schedule for another patient")]
    public async Task<IActionResult> ScheduleAppointment(
        int patientUserId,
        [FromBody] ScheduleAppointmentResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return Problem("You can only schedule appointments for yourself.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = ScheduleAppointmentCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            appointment => CreatedAtAction(nameof(ScheduleAppointment), new { patientUserId },
                ControlAppointmentResourceFromEntityAssembler.ToResourceFromEntity(appointment)));
    }
}
