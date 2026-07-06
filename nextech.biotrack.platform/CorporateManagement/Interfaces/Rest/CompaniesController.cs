using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.CorporateManagement.Application.CommandServices;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Companies endpoints")]
public class CompaniesController(ICompanyCommandService companyCommandService) : ControllerBase
{
    /// <summary>Register a new company (TS04)</summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Register company", OperationId = "RegisterCompany")]
    [SwaggerResponse(StatusCodes.Status201Created, "Company registered successfully", typeof(CompanyResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid RUC format")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "RUC already registered")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCompanyResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = RegisterCompanyCommandFromResourceAssembler.ToCommandFromResource(resource, currentUser.Id);
        var result = await companyCommandService.Handle(command, cancellationToken);
        return CorporateManagementActionResultAssembler.ToActionResult(
            this, result,
            company => StatusCode(StatusCodes.Status201Created,
                CompanyResourceFromEntityAssembler.ToResourceFromEntity(company)));
    }
}
