using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;

namespace nextech.biotrack.platform.ProgressTracking.Application.QueryServices;

public interface IProgressChartQueryService
{
    Task<ProgressChartResult?> Handle(GetProgressChartQuery query, CancellationToken cancellationToken);
}
