using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Application.CommandServices;
using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/auth")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthController(IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>Login with email and password (TS02)</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Login", OperationId = "Login")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated successfully", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid credentials")]
    public async Task<IActionResult> Login(
        [FromBody] LoginResource resource,
        CancellationToken cancellationToken)
    {
        var command = LoginCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, cancellationToken);
        return IamActionResultAssembler.ToActionResult(
            this, result,
            userAndToken => Ok(AuthenticatedUserResourceFromEntityAssembler
                .ToResourceFromEntity(userAndToken.user, userAndToken.token)));
    }

    /// <summary>Verify email address with token (TS03)</summary>
    [HttpGet("verify-email")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Verify email", OperationId = "VerifyEmail")]
    [SwaggerResponse(StatusCodes.Status200OK, "Email verified successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid or already used token")]
    public async Task<IActionResult> VerifyEmail(
        [FromQuery] string token,
        CancellationToken cancellationToken)
    {
        var command = new VerifyEmailCommand(token);
        var result = await userCommandService.Handle(command, cancellationToken);
        return IamActionResultAssembler.ToActionResult(
            this, result,
            () => Ok(new { message = "Email verified successfully." }));
    }
}
