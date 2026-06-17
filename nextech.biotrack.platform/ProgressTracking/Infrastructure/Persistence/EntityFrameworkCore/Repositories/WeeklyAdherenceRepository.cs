using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WeeklyAdherenceRepository(AppDbContext context)
    : BaseRepository<WeeklyAdherence>(context), IWeeklyAdherenceRepository
{
    public async Task<WeeklyAdherence?> FindByPatientAndWeekAsync(
        int patientUserId, int planId, DateOnly weekStart, CancellationToken cancellationToken)
    {
        return await Context.Set<WeeklyAdherence>()
            .FirstOrDefaultAsync(r => r.PatientUserId == patientUserId && r.PlanId == planId && r.WeekStart == weekStart,
                cancellationToken);
    }

    public async Task<IEnumerable<WeeklyAdherence>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        return await Context.Set<WeeklyAdherence>()
            .Where(r => r.PatientUserId == patientUserId && r.WeekStart >= start && r.WeekStart <= end)
            .OrderBy(r => r.WeekStart)
            .ToListAsync(cancellationToken);
    }
}
