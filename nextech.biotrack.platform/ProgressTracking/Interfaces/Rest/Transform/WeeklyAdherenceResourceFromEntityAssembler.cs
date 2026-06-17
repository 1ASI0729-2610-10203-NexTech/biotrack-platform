using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class WeeklyAdherenceResourceFromEntityAssembler
{
    public static WeeklyAdherenceResource ToResourceFromEntity(WeeklyAdherence entity)
    {
        return new WeeklyAdherenceResource(entity.Id, entity.PatientUserId, entity.PlanId, entity.WeekStart, entity.AdherencePct);
    }
}
