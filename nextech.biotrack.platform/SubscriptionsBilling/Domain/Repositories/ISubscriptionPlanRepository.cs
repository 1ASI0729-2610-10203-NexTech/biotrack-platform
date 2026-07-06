using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

public interface ISubscriptionPlanRepository : IBaseRepository<SubscriptionPlan>
{
    Task<SubscriptionPlan?> FindByNameAsync(string name, CancellationToken cancellationToken);
    Task<IEnumerable<SubscriptionPlan>> FindAllActiveAsync(CancellationToken cancellationToken);
}
