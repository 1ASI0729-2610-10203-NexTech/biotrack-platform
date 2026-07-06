namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

public record ReactivateSubscriptionCommand(int SubscriptionId, int RequestingUserId);
