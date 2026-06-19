using nextech.biotrack.platform.SubscriptionsBilling.Application.QueryServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Queries;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.QueryServices;

public class BillingSummaryQueryService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPlanRepository planRepository,
    IPaymentRepository paymentRepository,
    IInvoiceRepository invoiceRepository)
    : IBillingSummaryQueryService
{
    public async Task<BillingSummaryResult?> Handle(GetBillingSummaryQuery query, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(query.SubscriptionId, cancellationToken);
        if (subscription == null) return null;

        if (subscription.UserId != query.RequestingUserId) return null;

        var plan = await planRepository.FindByIdAsync(subscription.PlanId, cancellationToken);
        if (plan == null) return null;

        var payments = (await paymentRepository.FindBySubscriptionIdAsync(subscription.Id, cancellationToken))
            .OrderByDescending(p => p.PaymentDate).ToList();

        var pendingInvoices = await invoiceRepository.CountPendingBySubscriptionIdAsync(subscription.Id, cancellationToken);

        var paymentHistory = payments.Select(p => new PaymentHistoryItem(
            p.Id,
            p.PaymentDate,
            p.Amount,
            p.Status,
            p.TransactionId)).ToList();

        var outstandingBalance = payments
            .Where(p => p.Status == PaymentStatus.Rejected || p.Status == PaymentStatus.Pending)
            .Sum(p => p.Amount);

        return new BillingSummaryResult(
            subscription.Id,
            subscription.UserId,
            subscription.Status,
            plan.Name,
            plan.BillingCycle,
            plan.MonthlyAmount,
            subscription.StartDate,
            subscription.NextBillingDate,
            paymentHistory,
            pendingInvoices,
            outstandingBalance);
    }
}
