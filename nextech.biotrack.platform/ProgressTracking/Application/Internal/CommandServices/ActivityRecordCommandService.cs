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

public class ActivityRecordCommandService(
    IActivityRecordRepository repository,
    IUnitOfWork unitOfWork)
    : IActivityRecordCommandService
{
    public async Task<Result<ActivityRecord>> Handle(RegisterActivityCommand command, CancellationToken cancellationToken)
    {
        if (command.DurationMinutes <= 0)
            return Result<ActivityRecord>.Failure(ProgressTrackingError.InvalidDuration, "Duration must be greater than 0.");

        if (!Enum.TryParse<ActivityType>(command.ActivityType, true, out _))
            return Result<ActivityRecord>.Failure(ProgressTrackingError.InvalidActivityType,
                $"Activity type '{command.ActivityType}' is not valid.");

        if (!Enum.TryParse<ActivityIntensity>(command.Intensity, true, out _))
            return Result<ActivityRecord>.Failure(ProgressTrackingError.InvalidActivityIntensity,
                $"Intensity '{command.Intensity}' is not valid.");

        var record = new ActivityRecord(command.PatientUserId, command.Date, command.ActivityType, command.DurationMinutes, command.Intensity);

        try
        {
            await repository.AddAsync(record, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ActivityRecord>.Success(record);
        }
        catch (OperationCanceledException)
        {
            return Result<ActivityRecord>.Failure(ProgressTrackingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<ActivityRecord>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<ActivityRecord>.Failure(ProgressTrackingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
