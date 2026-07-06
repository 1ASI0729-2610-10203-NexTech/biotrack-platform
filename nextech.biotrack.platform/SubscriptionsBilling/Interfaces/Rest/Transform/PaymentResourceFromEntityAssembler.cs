using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.SubscriptionId,
            entity.PaymentDate,
            entity.Amount,
            entity.Status.ToString(),
            entity.TransactionId,
            entity.GatewayMessage);
    }
}
