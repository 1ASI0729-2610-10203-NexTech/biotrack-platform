using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;

public class InitialEvaluationCommandService(
    IInitialEvaluationRepository evaluationRepository,
    IUnitOfWork unitOfWork)
    : IInitialEvaluationCommandService
{
    public async Task<Result<InitialEvaluation>> Handle(
        CompleteEvaluationCommand command, CancellationToken cancellationToken)
    {
        if (command.CaloriesTarget <= 0)
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.InvalidCaloriesTarget,
                "The calories target must be greater than 0.");

        if (command.ProteinsPct + command.CarbohydratesPct + command.FatsPct != 100)
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.InvalidMacronutrients,
                "The macronutrient percentages must sum to 100.");

        if (await evaluationRepository.ExistsByPatientProfileIdAsync(command.PatientProfileId, cancellationToken))
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.EvaluationAlreadyExists,
                $"An initial evaluation already exists for patient profile {command.PatientProfileId}.");

        var evaluation = new InitialEvaluation(
            command.PatientProfileId, command.NutritionistUserId, command.Observations,
            command.CaloriesTarget, command.ProteinsPct, command.CarbohydratesPct, command.FatsPct);

        try
        {
            await evaluationRepository.AddAsync(evaluation, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<InitialEvaluation>.Success(evaluation);
        }
        catch (OperationCanceledException)
        {
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.OperationCancelled,
                "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while saving the evaluation.");
        }
        catch (Exception)
        {
            return Result<InitialEvaluation>.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }
}
