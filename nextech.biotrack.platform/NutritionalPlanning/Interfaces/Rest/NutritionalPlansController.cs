using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/nutritional-plans")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Nutritional Planning endpoints")]
public class NutritionalPlansController(
    INutritionalPlanCommandService commandService,
    INutritionalPlanQueryService queryService) : ControllerBase
{
    /// <summary>Get nutritional plans for the authenticated nutritionist (TS21)</summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Get nutritional plans", OperationId = "GetNutritionalPlans")]
    [SwaggerResponse(StatusCodes.Status200OK, "Plans retrieved", typeof(IEnumerable<NutritionalPlanResource>))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<IActionResult> GetPlans(CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var query = new GetNutritionalPlansByNutritionistIdQuery(currentUser.Id);
        var plans = await queryService.Handle(query, cancellationToken);
        var resources = plans.Select(NutritionalPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>Create a new nutritional plan (TS22)</summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Create nutritional plan", OperationId = "CreateNutritionalPlan")]
    [SwaggerResponse(StatusCodes.Status201Created, "Plan created", typeof(NutritionalPlanResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public async Task<IActionResult> CreatePlan(
        [FromBody] CreateNutritionalPlanResource resource,
        CancellationToken cancellationToken)
    {
        var currentUser = (User)HttpContext.Items["User"]!;
        var command = NutritionalPlanCommandFromResourceAssembler.ToCommandFromResource(resource, currentUser.Id);
        var result = await commandService.Handle(command, cancellationToken);
        return NutritionalPlanningActionResultAssembler.ToActionResult(
            this, result,
            plan => StatusCode(StatusCodes.Status201Created,
                NutritionalPlanResourceFromEntityAssembler.ToResourceFromEntity(plan)));
    }

    /// <summary>Get weekly diet for a nutritional plan (TS23)</summary>
    [HttpGet("{planId:int}/weekly-diet")]
    [SwaggerOperation(Summary = "Get weekly diet", OperationId = "GetWeeklyDiet")]
    [SwaggerResponse(StatusCodes.Status200OK, "Weekly diet returned")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Plan not found")]
    public async Task<IActionResult> GetWeeklyDiet(int planId, CancellationToken cancellationToken)
    {
        var query = new GetNutritionalPlanByIdQuery(planId);
        var plan = await queryService.Handle(query, cancellationToken);
        if (plan == null) return NotFound(new { message = $"Plan with ID {planId} not found." });

        var weeklyDiet = new[]
        {
            new { day = "Monday", meals = new[] { $"Breakfast: Oatmeal ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Grilled Chicken ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Salmon & Veggies ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Tuesday", meals = new[] { $"Breakfast: Eggs & Toast ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Turkey Wrap ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Pasta ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Wednesday", meals = new[] { $"Breakfast: Yogurt & Fruit ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Tuna Salad ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Grilled Steak ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Thursday", meals = new[] { $"Breakfast: Smoothie ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Chicken Salad ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Baked Fish ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Friday", meals = new[] { $"Breakfast: Pancakes ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Veggie Burger ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: BBQ Chicken ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Saturday", meals = new[] { $"Breakfast: Waffles ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Sushi ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Beef Stir-Fry ({(int)(plan.CalorieTarget * 0.30)} cal)" } },
            new { day = "Sunday", meals = new[] { $"Breakfast: French Toast ({(int)(plan.CalorieTarget * 0.25)} cal)", $"Lunch: Roast Chicken ({(int)(plan.CalorieTarget * 0.35)} cal)", $"Dinner: Vegetable Soup ({(int)(plan.CalorieTarget * 0.30)} cal)" } }
        };

        return Ok(new { planId = plan.Id, planName = plan.Name, calorieTarget = plan.CalorieTarget, days = weeklyDiet });
    }
}
