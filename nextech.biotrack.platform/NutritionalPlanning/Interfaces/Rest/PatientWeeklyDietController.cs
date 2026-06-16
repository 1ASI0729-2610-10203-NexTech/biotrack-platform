using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
public class PatientWeeklyDietController(
    INutritionalPlanQueryService queryService) : ControllerBase
{
    [HttpGet("{patientUserId:int}/weekly-diet")]
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
