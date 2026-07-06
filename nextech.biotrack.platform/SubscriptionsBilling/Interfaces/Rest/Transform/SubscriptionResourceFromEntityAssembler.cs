using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        return new SubscriptionResource(
            entity.Id,
            entity.UserId,
            entity.PlanId,
            entity.Status.ToString(),
            entity.StartDate,
            entity.NextBillingDate);
    }
}
