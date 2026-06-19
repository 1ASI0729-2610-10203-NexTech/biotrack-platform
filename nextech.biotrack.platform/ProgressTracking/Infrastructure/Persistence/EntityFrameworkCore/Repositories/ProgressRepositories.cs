using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WeightRecordRepository(AppDbContext context)
    : BaseRepository<WeightRecord>(context), IWeightRecordRepository
{
    public async Task<IEnumerable<WeightRecord>> FindByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await Context.Set<WeightRecord>()
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.RecordedAt)
            .ToListAsync(cancellationToken);
}

public class FoodEntryRepository(AppDbContext context)
    : BaseRepository<FoodEntry>(context), IFoodEntryRepository
{
    public async Task<IEnumerable<FoodEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await Context.Set<FoodEntry>()
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.LoggedAt)
            .ToListAsync(cancellationToken);
}

public class ActivityEntryRepository(AppDbContext context)
    : BaseRepository<ActivityEntry>(context), IActivityEntryRepository
{
    public async Task<IEnumerable<ActivityEntry>> FindByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await Context.Set<ActivityEntry>()
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.LoggedAt)
            .ToListAsync(cancellationToken);
}
