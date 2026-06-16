using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Patient Profile endpoints")]
public class PatientProfilesController(
    IPatientProfileCommandService patientProfileCommandService,
    IPatientProfileQueryService patientProfileQueryService) : ControllerBase
{
    /// <summary>Register health data for a patient (TS-PP01)</summary>
    [HttpPost("{patientUserId:int}/health-profile")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Register patient health data", OperationId = "RegisterHealthData")]
    [SwaggerResponse(StatusCodes.Status201Created, "Health profile created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid health data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Health profile already exists for this patient")]
    public async Task<IActionResult> RegisterHealthData(
        [FromRoute] int patientUserId,
        [FromBody] RegisterHealthDataResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return StatusCode(StatusCodes.Status403Forbidden,
                new { message = "You can only register health data for your own profile." });

        var command = RegisterHealthDataCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await patientProfileCommandService.Handle(command, cancellationToken);

        return PatientProfileActionResultAssembler.ToActionResult(this, result,
            profile => StatusCode(StatusCodes.Status201Created,
                new { id = profile.Id, patientUserId = profile.PatientUserId, bmi = profile.Bmi }));
    }

    /// <summary>Get the health profile of a patient (TS-PP02)</summary>
    [HttpGet("{patientUserId:int}/health-profile")]
    [SwaggerOperation(Summary = "Get patient health profile", OperationId = "GetHealthProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile retrieved successfully", typeof(PatientProfileResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Health profile not found")]
    public async Task<IActionResult> GetHealthProfile(
        [FromRoute] int patientUserId,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return StatusCode(StatusCodes.Status403Forbidden,
                new { message = "You can only view your own health profile." });

        var dto = await patientProfileQueryService.Handle(
            new GetPatientProfileQuery(patientUserId), cancellationToken);

        if (dto == null)
            return NotFound(new { message = $"No health profile found for patient {patientUserId}." });

        return Ok(PatientProfileResourceFromEntityAssembler.ToResourceFromDto(dto));
    }

    /// <summary>Define or update the nutritional goal of a patient (TS-PP03)</summary>
    [HttpPatch("{patientUserId:int}/health-profile/goal")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Define nutritional goal", OperationId = "DefineGoal")]
    [SwaggerResponse(StatusCodes.Status200OK, "Nutritional goal updated successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid goal value")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Health profile not found")]
    public async Task<IActionResult> DefineGoal(
        [FromRoute] int patientUserId,
        [FromBody] DefineGoalResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return StatusCode(StatusCodes.Status403Forbidden,
                new { message = "You can only update your own nutritional goal." });

        var command = DefineGoalCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await patientProfileCommandService.Handle(command, cancellationToken);

        return PatientProfileActionResultAssembler.ToActionResult(this, result,
            _ => Ok(new { message = "Nutritional goal updated successfully." }));
    }

    /// <summary>Register or replace food restrictions for a patient (TS-PP04)</summary>
    [HttpPatch("{patientUserId:int}/health-profile/restrictions")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Update food restrictions", OperationId = "UpdateFoodRestrictions")]
    [SwaggerResponse(StatusCodes.Status200OK, "Food restrictions updated successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid restriction data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "Access denied")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Health profile not found")]
    public async Task<IActionResult> UpdateFoodRestrictions(
        [FromRoute] int patientUserId,
        [FromBody] UpdateFoodRestrictionsResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        if (currentUser.Id != patientUserId)
            return StatusCode(StatusCodes.Status403Forbidden,
                new { message = "You can only update your own food restrictions." });

        var command = UpdateFoodRestrictionsCommandFromResourceAssembler.ToCommandFromResource(patientUserId, resource);
        var result = await patientProfileCommandService.Handle(command, cancellationToken);

        return PatientProfileActionResultAssembler.ToActionResult(this, result,
            () => Ok(new { message = "Food restrictions updated successfully." }));
    }
}
