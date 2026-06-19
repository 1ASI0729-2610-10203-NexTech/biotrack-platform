using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;

public partial class Subscription
{
    public Subscription() : this(0, 0, DateOnly.MinValue)
    {
    }

    public Subscription(int userId, int planId, DateOnly startDate)
    {
        UserId = userId;
        PlanId = planId;
        StartDate = startDate;
        Status = SubscriptionStatus.PendingPayment;
        NextBillingDate = startDate.AddMonths(1);
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int PlanId { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly NextBillingDate { get; private set; }
    public DateOnly? CancelledDate { get; private set; }

    public Subscription Activate(DateOnly nextBillingDate)
    {
        Status = SubscriptionStatus.Active;
        NextBillingDate = nextBillingDate;
        return this;
    }

    public Subscription Suspend()
    {
        Status = SubscriptionStatus.Suspended;
        return this;
    }

    public Subscription Reactivate(DateOnly nextBillingDate)
    {
        Status = SubscriptionStatus.Active;
        NextBillingDate = nextBillingDate;
        return this;
    }

    public Subscription SetPendingPayment()
    {
        Status = SubscriptionStatus.PendingPayment;
        return this;
    }

    public Subscription AdvanceBillingDate(DateOnly nextBillingDate)
    {
        NextBillingDate = nextBillingDate;
        return this;
    }
}
