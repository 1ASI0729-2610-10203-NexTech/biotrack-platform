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
[Route("api/v1/companies")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Company Collaborators endpoints")]
public class CompanyCollaboratorsController(ICompanyCommandService companyCommandService) : ControllerBase
{
    /// <summary>Upload collaborators for a company (TS05)</summary>
    [HttpPost("{companyId:int}/collaborators/upload")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Upload company collaborators", OperationId = "UploadCompanyCollaborators")]
    [SwaggerResponse(StatusCodes.Status202Accepted, "Collaborators upload accepted")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid collaborator data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied to this company")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Company not found")]
    public async Task<IActionResult> UploadCollaborators(
        [FromRoute] int companyId,
        [FromBody] UploadCollaboratorsResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = UploadCompanyCollaboratorsCommandFromResourceAssembler
            .ToCommandFromResource(companyId, currentUser.Id, resource);
        var result = await companyCommandService.Handle(command, cancellationToken);
        return CorporateManagementActionResultAssembler.ToActionResult(
            this, result,
            () => Accepted(new { message = "Collaborators uploaded successfully." }));
    }
}
