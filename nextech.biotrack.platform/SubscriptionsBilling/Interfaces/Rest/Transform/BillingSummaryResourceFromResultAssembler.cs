using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class BillingSummaryResourceFromResultAssembler
{
    public static BillingSummaryResource ToResourceFromResult(BillingSummaryResult result)
    {
        var paymentHistory = result.PaymentHistory
            .Select(PaymentHistoryItemResourceFromValueObjectAssembler.ToResourceFromValueObject);

        return new BillingSummaryResource(
            result.SubscriptionId,
            result.UserId,
            result.Status.ToString(),
            result.PlanName,
            result.BillingCycle.ToString(),
            result.MonthlyAmount,
            result.StartDate,
            result.NextBillingDate,
            paymentHistory,
            result.PendingInvoices,
            result.OutstandingBalance);
    }
}
