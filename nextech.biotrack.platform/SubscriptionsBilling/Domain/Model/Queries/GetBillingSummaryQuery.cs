namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Queries;

public record GetBillingSummaryQuery(int SubscriptionId, int RequestingUserId);
