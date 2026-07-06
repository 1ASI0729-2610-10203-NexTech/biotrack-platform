using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Queries;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.QueryServices;

public interface IBillingSummaryQueryService
{
    Task<BillingSummaryResult?> Handle(GetBillingSummaryQuery query, CancellationToken cancellationToken);
}
