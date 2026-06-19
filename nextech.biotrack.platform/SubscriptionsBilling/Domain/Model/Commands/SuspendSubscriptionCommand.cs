namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

public record SuspendSubscriptionCommand(int SubscriptionId, int RequestingUserId);
