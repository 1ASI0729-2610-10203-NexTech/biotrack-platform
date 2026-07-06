using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<IEnumerable<Invoice>> FindBySubscriptionIdAsync(int subscriptionId, CancellationToken cancellationToken);
    Task<int> CountPendingBySubscriptionIdAsync(int subscriptionId, CancellationToken cancellationToken);
}
