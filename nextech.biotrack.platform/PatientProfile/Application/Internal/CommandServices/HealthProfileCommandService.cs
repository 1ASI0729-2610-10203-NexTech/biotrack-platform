using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Application.Internal.CommandServices;

public class HealthProfileCommandService(
    IHealthProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IHealthProfileCommandService
{
    public async Task<Result<HealthProfile>> Handle(UpdateHealthDataCommand command, CancellationToken cancellationToken)
    {
        if (command.HeightCm <= 0 || command.WeightKg <= 0)
            return Result<HealthProfile>.Failure(PatientProfileError.InvalidHealthData, "Height and weight must be greater than zero.");

        var profile = await profileRepository.FindByUserIdAsync(command.UserId, cancellationToken);

        if (profile == null)
        {
            profile = new HealthProfile(command.UserId, command.HeightCm, command.WeightKg,
                command.GoalWeightKg, command.ActivityLevel, command.NutritionalObjective, string.Empty);
            try
            {
                await profileRepository.AddAsync(profile, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<HealthProfile>.Success(profile);
            }
            catch (DbUpdateException)
            {
                return Result<HealthProfile>.Failure(PatientProfileError.DatabaseError, "A database error occurred.");
            }
        }

        profile.UpdateHealthData(command.HeightCm, command.WeightKg, command.GoalWeightKg,
            command.ActivityLevel, command.NutritionalObjective);
        profileRepository.Update(profile);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HealthProfile>.Success(profile);
        }
        catch (DbUpdateException)
        {
            return Result<HealthProfile>.Failure(PatientProfileError.DatabaseError, "A database error occurred.");
        }
    }

    public async Task<Result<HealthProfile>> Handle(UpdateDietaryRestrictionsCommand command, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.FindByUserIdAsync(command.UserId, cancellationToken);
        if (profile == null)
            return Result<HealthProfile>.Failure(PatientProfileError.ProfileNotFound, $"Health profile for user {command.UserId} not found.");

        profile.UpdateDietaryRestrictions(command.Restrictions);
        profileRepository.Update(profile);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HealthProfile>.Success(profile);
        }
        catch (DbUpdateException)
        {
            return Result<HealthProfile>.Failure(PatientProfileError.DatabaseError, "A database error occurred.");
        }
    }
}
