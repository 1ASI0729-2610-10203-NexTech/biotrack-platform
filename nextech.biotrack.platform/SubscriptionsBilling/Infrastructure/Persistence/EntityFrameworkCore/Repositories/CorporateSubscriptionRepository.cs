using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CorporateSubscriptionRepository(AppDbContext context)
    : BaseRepository<CorporateSubscription>(context), ICorporateSubscriptionRepository
{
    public async Task<CorporateSubscription?> FindByOrganizationUserIdAsync(int organizationUserId, CancellationToken cancellationToken)
    {
        return await Context.Set<CorporateSubscription>()
            .FirstOrDefaultAsync(c => c.OrganizationUserId == organizationUserId, cancellationToken);
    }
}
