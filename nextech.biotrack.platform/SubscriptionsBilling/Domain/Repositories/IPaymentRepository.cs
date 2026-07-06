using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> FindBySubscriptionIdAsync(int subscriptionId, CancellationToken cancellationToken);
}
