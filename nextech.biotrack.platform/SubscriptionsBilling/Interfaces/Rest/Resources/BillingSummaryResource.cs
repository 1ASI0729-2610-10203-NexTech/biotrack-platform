namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record BillingSummaryResource(
    int SubscriptionId,
    int UserId,
    string Status,
    string PlanName,
    string BillingCycle,
    decimal MonthlyAmount,
    DateOnly StartDate,
    DateOnly NextBillingDate,
    IEnumerable<PaymentHistoryItemResource> PaymentHistory,
    int PendingInvoices,
    decimal OutstandingBalance);
