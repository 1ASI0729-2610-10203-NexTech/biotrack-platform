namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Events;

public record SubscriptionActivatedEvent(int SubscriptionId, int UserId, DateOnly StartDate, DateOnly NextBillingDate);
