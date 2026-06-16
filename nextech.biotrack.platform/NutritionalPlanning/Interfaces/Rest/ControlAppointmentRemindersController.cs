using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/control-appointments")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Control Appointment Reminders endpoints")]
public class ControlAppointmentRemindersController(
    IControlAppointmentCommandService commandService) : ControllerBase
{
    /// <summary>Send email reminders for appointments scheduled within the next 24 hours</summary>
    [HttpPost("reminders/send")]
    [SwaggerOperation(Summary = "Send appointment reminders", OperationId = "SendAppointmentReminders")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reminders processed successfully")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Insufficient permissions")]
    public async Task<IActionResult> SendReminders(CancellationToken cancellationToken)
    {
        var command = new SendReminderCommand();
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            () => Ok(new { message = "Reminders processed successfully." }));
    }
}
