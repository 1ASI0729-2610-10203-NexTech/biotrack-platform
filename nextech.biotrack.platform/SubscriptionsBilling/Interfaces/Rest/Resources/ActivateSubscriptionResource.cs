namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record ActivateSubscriptionResource(int PlanId, DateOnly StartDate);
