using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Application.CommandServices;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>Register a new user (TS01)</summary>
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register user", OperationId = "RegisterUser")]
    [SwaggerResponse(StatusCodes.Status201Created, "User registered successfully")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Email already registered")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserResource resource,
        CancellationToken cancellationToken)
    {
        var command = RegisterUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, cancellationToken);
        return IamActionResultAssembler.ToActionResult(
            this, result,
            () => StatusCode(StatusCodes.Status201Created, new { message = "User registered successfully." }));
    }
}
