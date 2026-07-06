using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IWeightRecordRepository : IBaseRepository<WeightRecord>
{
    Task<IEnumerable<WeightRecord>> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
}

public interface IFoodEntryRepository : IBaseRepository<FoodEntry>
{
    Task<IEnumerable<FoodEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
}

public interface IActivityEntryRepository : IBaseRepository<ActivityEntry>
{
    Task<IEnumerable<ActivityEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
}
