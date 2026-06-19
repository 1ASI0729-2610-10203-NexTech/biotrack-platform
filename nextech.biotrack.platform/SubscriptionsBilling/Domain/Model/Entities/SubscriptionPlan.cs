using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;

public class SubscriptionPlan
{
    public SubscriptionPlan() : this(string.Empty, PlanType.None, BillingCycle.None, 0m)
    {
    }

    public SubscriptionPlan(string name, PlanType planType, BillingCycle billingCycle, decimal monthlyAmount)
    {
        Name = name;
        PlanType = planType;
        BillingCycle = billingCycle;
        MonthlyAmount = monthlyAmount;
        IsActive = true;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public PlanType PlanType { get; private set; }
    public BillingCycle BillingCycle { get; private set; }
    public decimal MonthlyAmount { get; private set; }
    public bool IsActive { get; private set; }
}
