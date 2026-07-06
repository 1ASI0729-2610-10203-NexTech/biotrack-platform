using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;

public class NutritionalPlanCommandService(
    INutritionalPlanRepository planRepository,
    IUnitOfWork unitOfWork) : INutritionalPlanCommandService
{
    public async Task<Result<NutritionalPlan>> Handle(CreateNutritionalPlanCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidPlanName, "Plan name cannot be empty.");

        if (command.CalorieTarget <= 0)
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InvalidCalorieTarget, "Calorie target must be greater than zero.");

        var plan = new NutritionalPlan(command.Name, command.CalorieTarget, command.ProteinGrams,
            command.CarbsGrams, command.FatGrams, command.NutritionistId);

        try
        {
            await planRepository.AddAsync(plan, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<NutritionalPlan>.Success(plan);
        }
        catch (OperationCanceledException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<NutritionalPlan>.Failure(NutritionalPlanningError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
