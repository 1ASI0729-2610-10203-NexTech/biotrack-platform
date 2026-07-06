using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;

namespace nextech.biotrack.platform.ProgressTracking.Application.QueryServices;

public interface IProgressQueryService
{
    Task<IEnumerable<WeightRecord>> Handle(GetWeightProgressByUserIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<ActivityEntry>> Handle(GetActivityHistoryByUserIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<FoodEntry>> Handle(GetFoodLogsByUserIdQuery query, CancellationToken cancellationToken);
}
