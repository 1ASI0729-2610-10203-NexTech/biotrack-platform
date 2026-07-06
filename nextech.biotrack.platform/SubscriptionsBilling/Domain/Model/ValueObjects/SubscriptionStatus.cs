namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

public enum SubscriptionStatus
{
    None,
    Active,
    Suspended,
    PendingPayment,
    Cancelled,
    Expired
}
