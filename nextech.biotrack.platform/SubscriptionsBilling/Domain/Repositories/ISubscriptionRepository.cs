using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    Task<Subscription?> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<IEnumerable<Subscription>> FindAllByUserIdAsync(int userId, CancellationToken cancellationToken);
}
