using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/control-appointments")]
[Produces(MediaTypeNames.Application.Json)]
public class ControlAppointmentRemindersController(
    IControlAppointmentCommandService commandService) : ControllerBase
{
    [HttpPost("reminders/send")]
    public async Task<IActionResult> SendReminders(CancellationToken cancellationToken)
    {
        var command = new SendReminderCommand();
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(this, result,
            () => Ok(new { message = "Reminders processed successfully." }));
    }
}
