using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ActivityRecordRepository(AppDbContext context)
    : BaseRepository<ActivityRecord>(context), IActivityRecordRepository
{
    public async Task<IEnumerable<ActivityRecord>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        return await Context.Set<ActivityRecord>()
            .Where(r => r.PatientUserId == patientUserId && r.Date >= start && r.Date <= end)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ActivityRecord>> FindByPatientUserIdAndWeekAsync(
        int patientUserId, DateOnly weekStart, CancellationToken cancellationToken)
    {
        var weekEnd = weekStart.AddDays(6);
        return await Context.Set<ActivityRecord>()
            .Where(r => r.PatientUserId == patientUserId && r.Date >= weekStart && r.Date <= weekEnd)
            .ToListAsync(cancellationToken);
    }
}
