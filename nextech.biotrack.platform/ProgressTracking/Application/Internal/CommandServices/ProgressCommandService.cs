using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;

public class ProgressCommandService(
    IWeightRecordRepository weightRepository,
    IFoodEntryRepository foodRepository,
    IActivityEntryRepository activityRepository,
    IUnitOfWork unitOfWork) : IProgressCommandService
{
    public async Task<Result<WeightRecord>> Handle(RecordWeightCommand command, CancellationToken cancellationToken)
    {
        if (command.WeightKg <= 0)
            return Result<WeightRecord>.Failure(ProgressTrackingError.InvalidData, "Weight must be greater than zero.");

        var record = new WeightRecord(command.UserId, command.WeightKg, command.Notes);
        try
        {
            await weightRepository.AddAsync(record, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<WeightRecord>.Success(record);
        }
        catch (DbUpdateException)
        {
            return Result<WeightRecord>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
    }

    public async Task<Result<FoodEntry>> Handle(LogFoodCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.FoodName) || command.Calories <= 0)
            return Result<FoodEntry>.Failure(ProgressTrackingError.InvalidData, "Food name and calories are required.");

        var entry = new FoodEntry(command.UserId, command.MealType, command.FoodName, command.Calories);
        try
        {
            await foodRepository.AddAsync(entry, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<FoodEntry>.Success(entry);
        }
        catch (DbUpdateException)
        {
            return Result<FoodEntry>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
    }

    public async Task<Result<ActivityEntry>> Handle(LogActivityCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.ActivityType) || command.DurationMinutes <= 0)
            return Result<ActivityEntry>.Failure(ProgressTrackingError.InvalidData, "Activity type and duration are required.");

        var caloriesBurned = command.DurationMinutes * 8;
        var entry = new ActivityEntry(command.UserId, command.ActivityType, command.DurationMinutes, caloriesBurned);
        try
        {
            await activityRepository.AddAsync(entry, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ActivityEntry>.Success(entry);
        }
        catch (DbUpdateException)
        {
            return Result<ActivityEntry>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
    }
}
