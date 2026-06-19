using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;

public interface IProcessRenewalCommandService
{
    Task<Result<Subscription>> Handle(ProcessRenewalCommand command, CancellationToken cancellationToken);
}
