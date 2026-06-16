using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Entities;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;

public class NutritionalPlanCommandService(
    INutritionalPlanRepository planRepository,
    IUnitOfWork unitOfWork)
    : INutritionalPlanCommandService
{
    public async Task<Result<NutritionalPlan>> Handle(
        CreatePlanCommand command, CancellationToken cancellationToken)
    {
        if (command.PlanDurationDays <= 0)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidDayOrMeal,
                "The plan duration must be greater than 0.");

        var days = command.Days.ToList();
        if (days.Count == 0)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidDayOrMeal,
                "The plan must include at least one day.");

        foreach (var day in days)
        {
            var meals = day.Meals.ToList();
            if (meals.Count == 0)
                return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidDayOrMeal,
                    $"Day '{day.DayOfWeek}' must have at least one meal.");

            if (meals.Any(m => string.IsNullOrWhiteSpace(m.Name) || m.Calories <= 0))
                return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidDayOrMeal,
                    "Each meal must have a name and calories greater than 0.");
        }

        var plan = new NutritionalPlan(
            command.PatientProfileId, command.PatientUserId, command.NutritionistUserId,
            command.Title, command.Description, command.PlanDurationDays);

        foreach (var dayDto in days)
        {
            var planDay = new PlanDay(0, dayDto.DayOfWeek);
            foreach (var mealDto in dayDto.Meals)
                planDay.AddMeal(new Meal(0, mealDto.Type, mealDto.Name, mealDto.Description, mealDto.Calories));
            plan.AddDay(planDay);
        }

        try
        {
            await planRepository.AddAsync(plan, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<NutritionalPlan>.Success(plan);
        }
        catch (OperationCanceledException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.OperationCancelled,
                "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while saving the plan.");
        }
        catch (Exception)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    public async Task<Result<NutritionalPlan>> Handle(
        AcceptPlanCommand command, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdWithDaysAsync(command.PlanId, cancellationToken);

        if (plan == null)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.PlanNotFound,
                $"Nutritional plan {command.PlanId} was not found.");

        if (plan.PatientUserId != command.PatientUserId)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.AccessDenied,
                "You do not have permission to accept this plan.");

        if (!plan.IsProposed)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidPlanStatus,
                "Only plans in Proposed status can be accepted.");

        plan.Activate();
        planRepository.Update(plan);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<NutritionalPlan>.Success(plan);
        }
        catch (DbUpdateException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while updating the plan.");
        }
        catch (Exception)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    public async Task<Result<NutritionalPlan>> Handle(
        RejectPlanCommand command, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdWithDaysAsync(command.PlanId, cancellationToken);

        if (plan == null)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.PlanNotFound,
                $"Nutritional plan {command.PlanId} was not found.");

        if (plan.PatientUserId != command.PatientUserId)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.AccessDenied,
                "You do not have permission to reject this plan.");

        if (!plan.IsProposed)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidPlanStatus,
                "Only plans in Proposed status can be rejected.");

        plan.Reject(command.RejectionNotes);
        planRepository.Update(plan);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<NutritionalPlan>.Success(plan);
        }
        catch (DbUpdateException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while updating the plan.");
        }
        catch (Exception)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }
}
