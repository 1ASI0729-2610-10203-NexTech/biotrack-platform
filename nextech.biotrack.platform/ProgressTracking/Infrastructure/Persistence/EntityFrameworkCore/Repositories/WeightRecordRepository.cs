using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WeightRecordRepository(AppDbContext context)
    : BaseRepository<WeightRecord>(context), IWeightRecordRepository
{
    public async Task<WeightRecord?> FindByPatientUserIdAndDateAsync(
        int patientUserId, DateOnly date, CancellationToken cancellationToken)
    {
        return await Context.Set<WeightRecord>()
            .FirstOrDefaultAsync(r => r.PatientUserId == patientUserId && r.Date == date, cancellationToken);
    }

    public async Task<IEnumerable<WeightRecord>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        return await Context.Set<WeightRecord>()
            .Where(r => r.PatientUserId == patientUserId && r.Date >= start && r.Date <= end)
            .OrderBy(r => r.Date)
            .ToListAsync(cancellationToken);
    }
}
