using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SubscriptionPlanRepository(AppDbContext context)
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{
    public async Task<SubscriptionPlan?> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<SubscriptionPlan>()
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<SubscriptionPlan>> FindAllActiveAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<SubscriptionPlan>()
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }
}
