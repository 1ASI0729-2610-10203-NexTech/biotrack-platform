namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

public record PaymentResult(
    bool IsApproved,
    string TransactionId,
    string GatewayMessage);
