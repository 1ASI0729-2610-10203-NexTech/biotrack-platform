namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record PaymentHistoryItemResource(
    int PaymentId,
    DateOnly Date,
    decimal Amount,
    string Status,
    string TransactionId);
