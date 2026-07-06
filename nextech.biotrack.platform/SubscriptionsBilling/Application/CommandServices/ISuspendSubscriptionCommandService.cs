using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;

public interface ISuspendSubscriptionCommandService
{
    Task<Result<Subscription>> Handle(SuspendSubscriptionCommand command, CancellationToken cancellationToken);
}
