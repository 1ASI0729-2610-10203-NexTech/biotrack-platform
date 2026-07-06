using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HealthProfileRepository(AppDbContext context)
    : BaseRepository<HealthProfile>(context), IHealthProfileRepository
{
    public async Task<HealthProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await Context.Set<HealthProfile>()
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

    public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken) =>
        await Context.Set<HealthProfile>()
            .AnyAsync(p => p.UserId == userId, cancellationToken);
}
