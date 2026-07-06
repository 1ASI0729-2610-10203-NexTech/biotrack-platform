using nextech.biotrack.platform.ProgressTracking.Application.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.QueryServices;

public class ProgressQueryService(
    IWeightRecordRepository weightRepository,
    IFoodEntryRepository foodRepository,
    IActivityEntryRepository activityRepository) : IProgressQueryService
{
    public async Task<IEnumerable<WeightRecord>> Handle(GetWeightProgressByUserIdQuery query, CancellationToken cancellationToken) =>
        await weightRepository.FindByUserIdAsync(query.UserId, cancellationToken);

    public async Task<IEnumerable<ActivityEntry>> Handle(GetActivityHistoryByUserIdQuery query, CancellationToken cancellationToken) =>
        await activityRepository.FindByUserIdAsync(query.UserId, cancellationToken);

    public async Task<IEnumerable<FoodEntry>> Handle(GetFoodLogsByUserIdQuery query, CancellationToken cancellationToken) =>
        await foodRepository.FindByUserIdAsync(query.UserId, cancellationToken);
}
