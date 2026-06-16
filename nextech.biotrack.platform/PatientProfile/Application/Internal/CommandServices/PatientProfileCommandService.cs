using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Domain.Model.ValueObjects;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;
using HealthProfile = nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates.PatientProfile;

namespace nextech.biotrack.platform.PatientProfile.Application.Internal.CommandServices;

public class PatientProfileCommandService(
    IPatientProfileRepository patientProfileRepository,
    IUnitOfWork unitOfWork) : IPatientProfileCommandService
{
    public async Task<Result<HealthProfile>> Handle(
        RegisterHealthDataCommand command, CancellationToken cancellationToken)
    {
        if (!Imc.IsValidInputs(command.CurrentWeightKg, command.HeightCm) ||
            command.Age <= 0 || command.Age > 120 ||
            command.SystolicPressure <= 0 || command.DiastolicPressure <= 0 ||
            command.BasalGlucoseMgDl <= 0)
            return Result<HealthProfile>.Failure(
                PatientProfileError.InvalidHealthData,
                "Weight, height, age, blood pressure and glucose must be positive values.");

        if (!Enum.TryParse<BiologicalSex>(command.BiologicalSex, true, out _))
            return Result<HealthProfile>.Failure(
                PatientProfileError.InvalidHealthData,
                $"'{command.BiologicalSex}' is not a valid biological sex.");

        if (!Enum.TryParse<ActivityLevel>(command.ActivityLevel, true, out _))
            return Result<HealthProfile>.Failure(
                PatientProfileError.InvalidHealthData,
                $"'{command.ActivityLevel}' is not a valid activity level.");

        if (await patientProfileRepository.ExistsByPatientUserIdAsync(command.PatientUserId, cancellationToken))
            return Result<HealthProfile>.Failure(
                PatientProfileError.ProfileAlreadyExists,
                "A health profile already exists for this patient.");

        var profile = new HealthProfile(
            command.PatientUserId,
            command.CurrentWeightKg,
            command.HeightCm,
            command.Age,
            command.BiologicalSex,
            command.ActivityLevel,
            command.SystolicPressure,
            command.DiastolicPressure,
            command.BasalGlucoseMgDl);

        try
        {
            await patientProfileRepository.AddAsync(profile, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HealthProfile>.Success(profile);
        }
        catch (OperationCanceledException)
        {
            return Result<HealthProfile>.Failure(
                PatientProfileError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<HealthProfile>.Failure(
                PatientProfileError.DatabaseError, "A database error occurred while saving the health profile.");
        }
        catch (Exception)
        {
            return Result<HealthProfile>.Failure(
                PatientProfileError.InternalServerError, "An unexpected error occurred.");
        }
    }

    public async Task<Result<HealthProfile>> Handle(
        DefineGoalCommand command, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<NutritionalGoal>(command.Goal, true, out _))
            return Result<HealthProfile>.Failure(
                PatientProfileError.InvalidGoal,
                $"'{command.Goal}' is not a valid nutritional goal.");

        var profile = await patientProfileRepository.FindByPatientUserIdAsync(
            command.PatientUserId, cancellationToken);

        if (profile == null)
            return Result<HealthProfile>.Failure(
                PatientProfileError.ProfileNotFound, "The patient profile was not found.");

        profile.DefineGoal(command.Goal);
        patientProfileRepository.Update(profile);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HealthProfile>.Success(profile);
        }
        catch (DbUpdateException)
        {
            return Result<HealthProfile>.Failure(
                PatientProfileError.DatabaseError, "A database error occurred while updating the goal.");
        }
    }

    public async Task<Result> Handle(
        UpdateFoodRestrictionsCommand command, CancellationToken cancellationToken)
    {
        var restrictionList = command.Restrictions.ToList();

        if (restrictionList.Any(r =>
                string.IsNullOrWhiteSpace(r.Type) ||
                string.IsNullOrWhiteSpace(r.Description) ||
                string.IsNullOrWhiteSpace(r.Severity)))
            return Result.Failure(
                PatientProfileError.InvalidRestrictionData,
                "All restrictions must have type, description and severity.");

        if (restrictionList.Any(r => !Enum.TryParse<RestrictionType>(r.Type, true, out _)))
            return Result.Failure(
                PatientProfileError.InvalidRestrictionData, "One or more restriction types are invalid.");

        if (restrictionList.Any(r => !Enum.TryParse<Severity>(r.Severity, true, out _)))
            return Result.Failure(
                PatientProfileError.InvalidRestrictionData, "One or more severity values are invalid.");

        var profile = await patientProfileRepository.FindByPatientUserIdWithRestrictionsAsync(
            command.PatientUserId, cancellationToken);

        if (profile == null)
            return Result.Failure(
                PatientProfileError.ProfileNotFound, "The patient profile was not found.");

        profile.ReplaceRestrictions(restrictionList.Select(r => (r.Type, r.Description, r.Severity)));
        patientProfileRepository.Update(profile);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(
                PatientProfileError.DatabaseError, "A database error occurred while updating restrictions.");
        }
    }
}
