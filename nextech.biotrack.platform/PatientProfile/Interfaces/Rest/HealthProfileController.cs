using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/profile")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Patient Profile endpoints")]
public class HealthProfileController(
    IHealthProfileCommandService commandService,
    IHealthProfileQueryService queryService) : ControllerBase
{
    /// <summary>Get the authenticated user's health profile (TS11)</summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Get health profile", OperationId = "GetHealthProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile returned", typeof(HealthProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var query = new GetHealthProfileByUserIdQuery(currentUser.Id);
        var profile = await queryService.Handle(query, cancellationToken);

        if (profile == null)
            return NotFound(new { message = "Health profile not found. Please update your health data first." });

        return Ok(HealthProfileResourceFromEntityAssembler.ToResourceFromEntity(profile));
    }

    /// <summary>Update health data for the authenticated user (TS12)</summary>
    [HttpPut("health-data")]
    [SwaggerOperation(Summary = "Update health data", OperationId = "UpdateHealthData")]
    [SwaggerResponse(StatusCodes.Status200OK, "Health data updated", typeof(HealthProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid health data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<IActionResult> UpdateHealthData(
        [FromBody] UpdateHealthDataResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new UpdateHealthDataCommand(currentUser.Id, resource.HeightCm, resource.WeightKg,
            resource.GoalWeightKg, resource.ActivityLevel, resource.NutritionalObjective);
        var result = await commandService.Handle(command, cancellationToken);
        return PatientProfileActionResultAssembler.ToActionResult(
            this, result,
            profile => Ok(HealthProfileResourceFromEntityAssembler.ToResourceFromEntity(profile)));
    }

    /// <summary>Get nutritional goals summary (TS13)</summary>
    [HttpGet("nutritional-goals")]
    [SwaggerOperation(Summary = "Get nutritional goals", OperationId = "GetNutritionalGoals")]
    [SwaggerResponse(StatusCodes.Status200OK, "Nutritional goals returned")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetNutritionalGoals(CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var query = new GetHealthProfileByUserIdQuery(currentUser.Id);
        var profile = await queryService.Handle(query, cancellationToken);

        if (profile == null)
            return NotFound(new { message = "Health profile not found." });

        var goals = new[]
        {
            new { type = "Calories", target = profile.CalorieTarget(), current = (int)(profile.CalorieTarget() * 0.85m), unit = "kcal" },
            new { type = "Protein", target = profile.ProteinTarget(), current = (int)(profile.ProteinTarget() * 0.90m), unit = "g" },
            new { type = "Carbohydrates", target = profile.CarbsTarget(), current = (int)(profile.CarbsTarget() * 0.80m), unit = "g" },
            new { type = "Fat", target = profile.FatTarget(), current = (int)(profile.FatTarget() * 0.75m), unit = "g" }
        };

        return Ok(new { userId = currentUser.Id, bmi = profile.Bmi, goals });
    }

    /// <summary>Update dietary restrictions (TS14)</summary>
    [HttpPut("restrictions")]
    [SwaggerOperation(Summary = "Update dietary restrictions", OperationId = "UpdateDietaryRestrictions")]
    [SwaggerResponse(StatusCodes.Status200OK, "Restrictions updated", typeof(HealthProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> UpdateRestrictions(
        [FromBody] UpdateDietaryRestrictionsResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new UpdateDietaryRestrictionsCommand(currentUser.Id, resource.Restrictions);
        var result = await commandService.Handle(command, cancellationToken);
        return PatientProfileActionResultAssembler.ToActionResult(
            this, result,
            profile => Ok(HealthProfileResourceFromEntityAssembler.ToResourceFromEntity(profile)));
    }
}
