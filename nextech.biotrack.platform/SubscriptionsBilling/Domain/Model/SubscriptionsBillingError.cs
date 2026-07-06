namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model;

public enum SubscriptionsBillingError
{
    None,
    SubscriptionNotFound,
    PlanNotFound,
    PaymentFailed,
    PaymentRejected,
    SubscriptionAlreadyActive,
    SubscriptionNotSuspended,
    SubscriptionNotEligibleForReactivation,
    AccessDenied,
    InvalidStartDate,
    InvalidSubscriptionId,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
