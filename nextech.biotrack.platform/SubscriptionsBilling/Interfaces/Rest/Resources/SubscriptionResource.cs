using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record SubscriptionResource(
    int Id,
    int UserId,
    int PlanId,
    string Status,
    DateOnly StartDate,
    DateOnly NextBillingDate);
