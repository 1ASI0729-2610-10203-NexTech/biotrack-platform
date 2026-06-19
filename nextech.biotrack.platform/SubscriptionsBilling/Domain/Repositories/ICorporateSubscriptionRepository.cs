using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

public interface ICorporateSubscriptionRepository : IBaseRepository<CorporateSubscription>
{
    Task<CorporateSubscription?> FindByOrganizationUserIdAsync(int organizationUserId, CancellationToken cancellationToken);
}
