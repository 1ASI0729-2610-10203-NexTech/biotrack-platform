namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Events;

public record PaymentRejectedEvent(int PaymentId, int SubscriptionId, decimal Amount, string GatewayMessage);
