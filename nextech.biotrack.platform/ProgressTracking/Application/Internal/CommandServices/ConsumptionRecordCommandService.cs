using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;

public class ConsumptionRecordCommandService(
    IConsumptionRecordRepository repository,
    IUnitOfWork unitOfWork)
    : IConsumptionRecordCommandService
{
    public async Task<Result<ConsumptionRecord>> Handle(RegisterConsumptionCommand command, CancellationToken cancellationToken)
    {
        if (command.Calories <= 0)
            return Result<ConsumptionRecord>.Failure(ProgressTrackingError.InvalidCalories, "Calories must be greater than 0.");

        if (!Enum.TryParse<MealType>(command.MealType, true, out _))
            return Result<ConsumptionRecord>.Failure(ProgressTrackingError.InvalidMealType,
                $"Meal type '{command.MealType}' is not valid.");

        var record = new ConsumptionRecord(command.PatientUserId, command.Date, command.MealType, command.Description, command.Calories);

        try
        {
            await repository.AddAsync(record, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ConsumptionRecord>.Success(record);
        }
        catch (OperationCanceledException)
        {
            return Result<ConsumptionRecord>.Failure(ProgressTrackingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<ConsumptionRecord>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<ConsumptionRecord>.Failure(ProgressTrackingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
