namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Events;

public record SubscriptionSuspendedEvent(int SubscriptionId, int UserId);
