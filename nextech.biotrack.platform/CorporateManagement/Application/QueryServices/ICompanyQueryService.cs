using nextech.biotrack.platform.CorporateManagement.Domain.Model;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Queries;

namespace nextech.biotrack.platform.CorporateManagement.Application.QueryServices;

public interface ICompanyQueryService
{
    Task<Company?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken);
    Task<CorporateMetrics?> Handle(GetCorporateMetricsByCompanyIdQuery query, CancellationToken cancellationToken);
}
