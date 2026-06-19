namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

public record ProcessRenewalCommand(int SubscriptionId, int RequestingUserId);
