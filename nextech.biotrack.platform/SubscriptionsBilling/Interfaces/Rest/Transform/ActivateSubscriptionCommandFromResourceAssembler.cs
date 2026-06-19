using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Transform;

public static class ActivateSubscriptionCommandFromResourceAssembler
{
    public static ActivateSubscriptionCommand ToCommandFromResource(int userId, ActivateSubscriptionResource resource)
    {
        return new ActivateSubscriptionCommand(userId, resource.PlanId, resource.StartDate);
    }
}
