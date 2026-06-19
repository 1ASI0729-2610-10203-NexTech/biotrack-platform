namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;

public record ActivateSubscriptionCommand(int UserId, int PlanId, DateOnly StartDate);
