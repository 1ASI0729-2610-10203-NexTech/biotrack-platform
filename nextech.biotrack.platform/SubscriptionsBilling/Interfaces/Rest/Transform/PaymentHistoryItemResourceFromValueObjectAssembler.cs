using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class PaymentHistoryItemResourceFromValueObjectAssembler
{
    public static PaymentHistoryItemResource ToResourceFromValueObject(PaymentHistoryItem item)
    {
        return new PaymentHistoryItemResource(
            item.PaymentId,
            item.Date,
            item.Amount,
            item.Status.ToString(),
            item.TransactionId);
    }
}
