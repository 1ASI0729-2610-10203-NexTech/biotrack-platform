using nextech.biotrack.platform.CorporateManagement.Domain.Model;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;

public static class CorporateMetricsResourceFromEntityAssembler
{
    public static CorporateMetricsResource ToResourceFromEntity(CorporateMetrics metrics) =>
        new(metrics.CompanyId,
            metrics.CompanyName,
            metrics.TotalCollaborators,
            metrics.ActiveCollaborators,
            metrics.InactiveCollaborators,
            metrics.PendingCollaborators,
            metrics.LastUpdated);
}
