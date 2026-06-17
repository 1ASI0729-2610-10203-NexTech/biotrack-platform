using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.ProgressTracking.Application.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient Progress Charts endpoints")]
public class PatientProgressChartsController(IProgressChartQueryService queryService) : ControllerBase
{
    /// <summary>Get the progress chart for a patient over a given period</summary>
    [HttpGet("{patientUserId:int}/progress-chart")]
    [SwaggerOperation(Summary = "Get progress chart", OperationId = "GetProgressChart")]
    [SwaggerResponse(StatusCodes.Status200OK, "Progress chart data returned", typeof(ProgressChartResource))]
    [SwaggerResponse(StatusCodes.Status204NoContent, "No progress data available for the given period")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    public async Task<IActionResult> GetProgressChart(
        int patientUserId,
        [FromQuery] DateOnly periodStart,
        [FromQuery] DateOnly periodEnd,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId && currentUser.Role != "NUTRITIONIST")
            return Problem("You do not have permission to view this patient's progress chart.",
                statusCode: StatusCodes.Status403Forbidden);

        var query = new GetProgressChartQuery(patientUserId, periodStart, periodEnd);
        var dto = await queryService.Handle(query, cancellationToken);
        if (dto == null) return NoContent();
        return Ok(ProgressChartResourceFromDtoAssembler.ToResourceFromDto(dto));
    }
}
