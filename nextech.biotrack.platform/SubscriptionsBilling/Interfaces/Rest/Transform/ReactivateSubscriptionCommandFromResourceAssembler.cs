using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class ReactivateSubscriptionCommandFromResourceAssembler
{
    public static ReactivateSubscriptionCommand ToCommandFromResource(int subscriptionId, int requestingUserId)
    {
        return new ReactivateSubscriptionCommand(subscriptionId, requestingUserId);
    }
}
