namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record PaymentResource(
    int Id,
    int SubscriptionId,
    DateOnly PaymentDate,
    decimal Amount,
    string Status,
    string TransactionId,
    string GatewayMessage);
