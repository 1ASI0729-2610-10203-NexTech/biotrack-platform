using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class CalculateAdherenceCommandFromResourceAssembler
{
    public static CalculateAdherenceCommand ToCommandFromResource(int patientUserId, CalculateAdherenceResource resource)
    {
        return new CalculateAdherenceCommand(patientUserId, resource.PlanId, resource.WeekStart);
    }
}
