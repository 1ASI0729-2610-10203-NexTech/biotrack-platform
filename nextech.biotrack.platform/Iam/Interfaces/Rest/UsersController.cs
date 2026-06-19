using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Application.CommandServices;
using nextech.biotrack.platform.Iam.Application.QueryServices;
using nextech.biotrack.platform.Iam.Domain.Model.Queries;
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
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : ControllerBase
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

    /// <summary>Get user by ID</summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get user by ID", OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(id), cancellationToken);
        if (user is null) return NotFound(new { message = "User not found." });
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }
}
