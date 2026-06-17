using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class EvolutionReportResourceFromEntityAssembler
{
    public static EvolutionReportResource ToResourceFromEntity(EvolutionReport entity)
    {
        return new EvolutionReportResource(entity.Id, entity.PatientUserId, entity.PeriodStart, entity.PeriodEnd, entity.Status);
    }
}
