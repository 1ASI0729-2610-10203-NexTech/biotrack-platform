using nextech.biotrack.platform.PatientProfile.Application.Internal.Model;
using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;
using nextech.biotrack.platform.PatientProfile.Domain.Model.ValueObjects;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Application.Internal.QueryServices;

public class PatientProfileQueryService(IPatientProfileRepository patientProfileRepository)
    : IPatientProfileQueryService
{
    public async Task<PatientProfileDto?> Handle(
        GetPatientProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository
            .FindByPatientUserIdWithRestrictionsAsync(query.PatientUserId, cancellationToken);

        if (profile == null) return null;

        return new PatientProfileDto(
            profile.Id,
            profile.PatientUserId,
            profile.CurrentWeightKg,
            profile.HeightCm,
            profile.Age,
            profile.BiologicalSex,
            profile.ActivityLevel,
            profile.SystolicPressure,
            profile.DiastolicPressure,
            profile.BasalGlucoseMgDl,
            profile.Bmi,
            profile.BmiCategory,
            profile.NutritionalGoal,
            profile.GoalStatus,
            profile.IsComplete,
            profile.FoodRestrictions.Select(r => new FoodRestrictionDto(r.Type, r.Description, r.Severity)),
            profile.CreatedAt,
            profile.UpdatedAt);
    }
}
