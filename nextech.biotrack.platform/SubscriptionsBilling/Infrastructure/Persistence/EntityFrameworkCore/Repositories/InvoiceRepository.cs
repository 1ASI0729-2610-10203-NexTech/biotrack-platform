using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InvoiceRepository(AppDbContext context)
    : BaseRepository<Invoice>(context), IInvoiceRepository
{
    public async Task<IEnumerable<Invoice>> FindBySubscriptionIdAsync(int subscriptionId, CancellationToken cancellationToken)
    {
        return await Context.Set<Invoice>()
            .Where(i => i.SubscriptionId == subscriptionId)
            .OrderByDescending(i => i.IssuedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountPendingBySubscriptionIdAsync(int subscriptionId, CancellationToken cancellationToken)
    {
        return await Context.Set<Invoice>()
            .CountAsync(i => i.SubscriptionId == subscriptionId && !i.IsPaid, cancellationToken);
    }
}
