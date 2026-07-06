using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class HealthProfileResourceFromEntityAssembler
{
    public static HealthProfileResource ToResourceFromEntity(HealthProfile profile) =>
        new(profile.Id, profile.UserId, profile.HeightCm, profile.WeightKg,
            profile.GoalWeightKg, profile.Bmi, profile.ActivityLevel,
            profile.NutritionalObjective, profile.DietaryRestrictions, profile.UpdatedAt);
}
