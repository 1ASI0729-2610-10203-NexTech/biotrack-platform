using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class UpdateWeightCommandFromResourceAssembler
{
    public static UpdateWeightCommand ToCommandFromResource(int patientUserId, UpdateWeightResource resource)
    {
        return new UpdateWeightCommand(patientUserId, resource.Date, resource.WeightKg);
    }
}
