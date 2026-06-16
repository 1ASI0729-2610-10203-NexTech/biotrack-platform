using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.CorporateManagement.Application.QueryServices;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Queries;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/companies")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Corporate Metrics endpoints")]
public class CorporateMetricsController(ICompanyQueryService companyQueryService) : ControllerBase
{
    /// <summary>Get anonymized corporate metrics for a company (TS06)</summary>
    [HttpGet("{companyId:int}/metrics")]
    [SwaggerOperation(Summary = "Get corporate metrics", OperationId = "GetCorporateMetrics")]
    [SwaggerResponse(StatusCodes.Status200OK, "Metrics retrieved successfully", typeof(CorporateMetricsResource))]
    [SwaggerResponse(StatusCodes.Status204NoContent, "No collaborator data available yet")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied to this company")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
    public async Task<IActionResult> GetMetrics(
        [FromRoute] int companyId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;

        var company = await companyQueryService.Handle(new GetCompanyByIdQuery(companyId), cancellationToken);
        if (company == null)
            return NotFound(new { message = $"Company with ID {companyId} was not found." });
        if (company.OwnerId != currentUser.Id)
            return StatusCode(StatusCodes.Status403Forbidden,
                new { message = "You do not have access to this company." });

        var metrics = await companyQueryService.Handle(
            new GetCorporateMetricsByCompanyIdQuery(companyId), cancellationToken);

        if (metrics == null || metrics.TotalCollaborators == 0)
            return NoContent();

        return Ok(CorporateMetricsResourceFromEntityAssembler.ToResourceFromEntity(metrics));
    }
}
