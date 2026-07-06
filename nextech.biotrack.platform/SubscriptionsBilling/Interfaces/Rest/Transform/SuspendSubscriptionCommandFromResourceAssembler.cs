using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class SuspendSubscriptionCommandFromResourceAssembler
{
    public static SuspendSubscriptionCommand ToCommandFromResource(int subscriptionId, int requestingUserId)
    {
        return new SuspendSubscriptionCommand(subscriptionId, requestingUserId);
    }
}
