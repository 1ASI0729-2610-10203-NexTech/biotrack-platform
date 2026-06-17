using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;

public class WeightRecordCommandService(
    IWeightRecordRepository repository,
    IUnitOfWork unitOfWork)
    : IWeightRecordCommandService
{
    public async Task<Result<WeightRecord>> Handle(UpdateWeightCommand command, CancellationToken cancellationToken)
    {
        if (command.WeightKg <= 0)
            return Result<WeightRecord>.Failure(ProgressTrackingError.InvalidWeight, "Weight must be greater than 0.");

        var existing = await repository.FindByPatientUserIdAndDateAsync(command.PatientUserId, command.Date, cancellationToken);

        try
        {
            WeightRecord record;
            if (existing != null)
            {
                existing.UpdateWeight(command.WeightKg);
                repository.Update(existing);
                record = existing;
            }
            else
            {
                record = new WeightRecord(command.PatientUserId, command.Date, command.WeightKg);
                await repository.AddAsync(record, cancellationToken);
            }

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<WeightRecord>.Success(record);
        }
        catch (OperationCanceledException)
        {
            return Result<WeightRecord>.Failure(ProgressTrackingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<WeightRecord>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<WeightRecord>.Failure(ProgressTrackingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
