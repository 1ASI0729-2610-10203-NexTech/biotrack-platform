using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class UpdateFoodRestrictionsCommandFromResourceAssembler
{
    public static UpdateFoodRestrictionsCommand ToCommandFromResource(
        int patientUserId, UpdateFoodRestrictionsResource resource) =>
        new(patientUserId,
            resource.Restrictions.Select(r => new RestrictionData(r.Type, r.Description, r.Severity)));
}
