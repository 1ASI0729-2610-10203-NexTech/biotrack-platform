using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class EvolutionReportRepository(AppDbContext context)
    : BaseRepository<EvolutionReport>(context), IEvolutionReportRepository
{
    public async Task<IEnumerable<EvolutionReport>> FindByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken)
    {
        return await Context.Set<EvolutionReport>()
            .Where(r => r.PatientUserId == patientUserId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
