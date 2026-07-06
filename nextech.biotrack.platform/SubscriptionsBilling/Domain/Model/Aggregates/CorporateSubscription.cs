using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;

public partial class CorporateSubscription
{
    public CorporateSubscription() : this(0, 0, string.Empty, DateOnly.MinValue)
    {
    }

    public CorporateSubscription(int organizationUserId, int planId, string organizationName, DateOnly startDate)
    {
        OrganizationUserId = organizationUserId;
        PlanId = planId;
        OrganizationName = organizationName;
        StartDate = startDate;
        Status = SubscriptionStatus.PendingPayment;
        NextBillingDate = startDate.AddMonths(1);
        MaxSeats = 0;
        UsedSeats = 0;
    }

    public int Id { get; private set; }
    public int OrganizationUserId { get; private set; }
    public int PlanId { get; private set; }
    public string OrganizationName { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly NextBillingDate { get; private set; }
    public int MaxSeats { get; private set; }
    public int UsedSeats { get; private set; }

    public CorporateSubscription Activate(DateOnly nextBillingDate, int maxSeats)
    {
        Status = SubscriptionStatus.Active;
        NextBillingDate = nextBillingDate;
        MaxSeats = maxSeats;
        return this;
    }

    public CorporateSubscription Suspend()
    {
        Status = SubscriptionStatus.Suspended;
        return this;
    }

    public CorporateSubscription Reactivate(DateOnly nextBillingDate)
    {
        Status = SubscriptionStatus.Active;
        NextBillingDate = nextBillingDate;
        return this;
    }
}
