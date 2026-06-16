using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient Weekly Diet endpoints")]
public class PatientWeeklyDietController(
    INutritionalPlanQueryService queryService) : ControllerBase
{
    /// <summary>Get the active weekly diet for a patient</summary>
    [HttpGet("{patientUserId:int}/weekly-diet")]
    [SwaggerOperation(Summary = "Get weekly diet", OperationId = "GetWeeklyDiet")]
    [SwaggerResponse(StatusCodes.Status200OK, "Active plan returned", typeof(WeeklyDietResource))]
    [SwaggerResponse(StatusCodes.Status204NoContent, "No active plan found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Missing or invalid token")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    public async Task<IActionResult> GetWeeklyDiet(
        int patientUserId,
        CancellationToken cancellationToken)
    {
        var query = new GetWeeklyDietQuery(patientUserId);
        var dto = await queryService.Handle(query, cancellationToken);
        if (dto == null) return NoContent();
        return Ok(WeeklyDietResourceFromDtoAssembler.ToResourceFromDto(dto));
    }
}
