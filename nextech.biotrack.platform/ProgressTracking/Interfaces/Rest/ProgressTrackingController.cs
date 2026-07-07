using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Application.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/progress")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Progress Tracking endpoints")]
public class ProgressTrackingController(
    IProgressCommandService commandService,
    IProgressQueryService queryService) : ControllerBase
{
    /// <summary>Get weight progress charts for the authenticated user (TS31)</summary>
    [HttpGet("charts")]
    [SwaggerOperation(Summary = "Get progress charts", OperationId = "GetProgressCharts")]
    [SwaggerResponse(StatusCodes.Status200OK, "Progress charts returned")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<IActionResult> GetCharts(CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var weightRecords = await queryService.Handle(new GetWeightProgressByUserIdQuery(currentUser.Id), cancellationToken);
        var activityEntries = await queryService.Handle(new GetActivityHistoryByUserIdQuery(currentUser.Id), cancellationToken);

        var weightData = weightRecords.Select(r => r.WeightKg).ToArray();
        var activityData = activityEntries.Select(a => (decimal)a.CaloriesBurned).ToArray();

        return Ok(new
        {
            charts = new[]
            {
                new { name = "Weight Progress", data = weightData.Length > 0 ? weightData : new[] { 80m, 78.5m, 77m, 75.5m, 74m } },
                new { name = "Activity Calories", data = activityData.Length > 0 ? activityData : new[] { 350m, 450m, 200m, 400m, 320m } }
            }
        });
    }

    /// <summary>Log food intake (TS32)</summary>
    [HttpPost("food-entries")]
    [SwaggerOperation(Summary = "Create food entry", OperationId = "CreateFoodEntry")]
    [SwaggerResponse(StatusCodes.Status201Created, "Food entry created", typeof(FoodEntryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> LogFood(
        [FromBody] LogFoodResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new LogFoodCommand(currentUser.Id, resource.MealType, resource.FoodName, resource.Calories);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(
            this, result,
            entry => StatusCode(StatusCodes.Status201Created,
                new FoodEntryResource(entry.Id, entry.MealType, entry.FoodName, entry.Calories, entry.LoggedAt)));
    }

    /// <summary>Log physical activity (TS33)</summary>
    [HttpPost("activity-entries")]
    [SwaggerOperation(Summary = "Create activity entry", OperationId = "CreateActivityEntry")]
    [SwaggerResponse(StatusCodes.Status201Created, "Activity entry created", typeof(ActivityEntryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> LogActivity(
        [FromBody] LogActivityResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new LogActivityCommand(currentUser.Id, resource.ActivityType, resource.DurationMinutes);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(
            this, result,
            entry => StatusCode(StatusCodes.Status201Created,
                new ActivityEntryResource(entry.Id, entry.ActivityType, entry.DurationMinutes, entry.CaloriesBurned, entry.LoggedAt)));
    }

    /// <summary>Record current weight (TS34)</summary>
    [HttpPost("weight-records")]
    [SwaggerOperation(Summary = "Record weight", OperationId = "RecordWeight")]
    [SwaggerResponse(StatusCodes.Status201Created, "Weight recorded", typeof(WeightRecordResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> RecordWeight(
        [FromBody] RecordWeightResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = new RecordWeightCommand(currentUser.Id, resource.WeightKg, resource.Notes);
        var result = await commandService.Handle(command, cancellationToken);
        return ProgressTrackingActionResultAssembler.ToActionResult(
            this, result,
            record => StatusCode(StatusCodes.Status201Created,
                new WeightRecordResource(record.Id, record.WeightKg, record.Notes, record.RecordedAt)));
    }

    /// <summary>Get activity history for the authenticated user (TS35)</summary>
    [HttpGet("activity-history")]
    [SwaggerOperation(Summary = "Get activity history", OperationId = "GetActivityHistory")]
    [SwaggerResponse(StatusCodes.Status200OK, "Activity history returned", typeof(IEnumerable<ActivityEntryResource>))]
    public async Task<IActionResult> GetActivityHistory(CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var entries = await queryService.Handle(new GetActivityHistoryByUserIdQuery(currentUser.Id), cancellationToken);
        var resources = entries.Select(e => new ActivityEntryResource(e.Id, e.ActivityType, e.DurationMinutes, e.CaloriesBurned, e.LoggedAt));
        return Ok(resources);
    }
}
