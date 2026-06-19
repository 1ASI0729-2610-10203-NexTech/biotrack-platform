namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

public record BillingSummaryResult(
    int SubscriptionId,
    int UserId,
    SubscriptionStatus Status,
    string PlanName,
    BillingCycle BillingCycle,
    decimal MonthlyAmount,
    DateOnly StartDate,
    DateOnly NextBillingDate,
    IEnumerable<PaymentHistoryItem> PaymentHistory,
    int PendingInvoices,
    decimal OutstandingBalance);

public record PaymentHistoryItem(
    int PaymentId,
    DateOnly Date,
    decimal Amount,
    PaymentStatus Status,
    string TransactionId);
