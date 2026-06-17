using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient Evolution Reports endpoints")]
public class PatientEvolutionReportsController(IEvolutionReportCommandService commandService) : ControllerBase
{
    /// <summary>Generate an evolution report PDF for a patient over a given period</summary>
    [HttpGet("{patientUserId:int}/evolution-report")]
    [SwaggerOperation(Summary = "Generate evolution report", OperationId = "GenerateEvolutionReport")]
    [SwaggerResponse(StatusCodes.Status200OK, "PDF report returned (application/pdf)")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Insufficient data to generate report")]
    public async Task<IActionResult> GenerateEvolutionReport(
        int patientUserId,
        [FromQuery] DateOnly periodStart,
        [FromQuery] DateOnly periodEnd,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId && currentUser.Role != "NUTRITIONIST")
            return Problem("You do not have permission to generate a report for this patient.",
                statusCode: StatusCodes.Status403Forbidden);

        var command = new GenerateReportCommand(patientUserId, currentUser.Id, periodStart, periodEnd);
        var result = await commandService.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            var error = (ProgressTrackingError)result.Error!;
            var statusCode = error == ProgressTrackingError.InsufficientProgressData
                ? StatusCodes.Status422UnprocessableEntity
                : StatusCodes.Status500InternalServerError;
            return Problem(result.Message, statusCode: statusCode);
        }

        return File(result.Value!.PdfContent!, "application/pdf",
            $"evolution-report-{patientUserId}-{periodStart:yyyy-MM-dd}_{periodEnd:yyyy-MM-dd}.pdf");
    }
}
