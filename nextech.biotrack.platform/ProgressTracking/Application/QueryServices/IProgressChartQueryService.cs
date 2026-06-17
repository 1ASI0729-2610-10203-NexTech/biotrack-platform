using nextech.biotrack.platform.ProgressTracking.Application.Dtos;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;

namespace nextech.biotrack.platform.ProgressTracking.Application.QueryServices;

public interface IProgressChartQueryService
{
    Task<ProgressChartDto?> Handle(GetProgressChartQuery query, CancellationToken cancellationToken);
}
