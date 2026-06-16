using nextech.biotrack.platform.PatientProfile.Application.Internal.Model;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class PatientProfileResourceFromEntityAssembler
{
    public static PatientProfileResource ToResourceFromDto(PatientProfileDto dto) =>
        new(dto.Id,
            dto.PatientUserId,
            dto.CurrentWeightKg,
            dto.HeightCm,
            dto.Age,
            dto.BiologicalSex,
            dto.ActivityLevel,
            dto.SystolicPressure,
            dto.DiastolicPressure,
            dto.BasalGlucoseMgDl,
            dto.Bmi,
            dto.BmiCategory,
            dto.NutritionalGoal,
            dto.GoalStatus,
            dto.IsComplete,
            dto.FoodRestrictions.Select(r => new FoodRestrictionItemResource(r.Type, r.Description, r.Severity)),
            dto.CreatedAt,
            dto.UpdatedAt);
}
