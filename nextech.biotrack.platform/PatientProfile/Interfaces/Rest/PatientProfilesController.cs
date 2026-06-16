using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/patients")]
[Produces("application/json")]
public class PatientProfilesController(
    IPatientProfileCommandService patientProfileCommandService,
    IPatientProfileQueryService patientProfileQueryService) : ControllerBase
{
    [HttpPost("{patientUserId:int}/health-profile")]
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

    [HttpGet("{patientUserId:int}/health-profile")]
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

    [HttpPatch("{patientUserId:int}/health-profile/goal")]
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

    [HttpPatch("{patientUserId:int}/health-profile/restrictions")]
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
