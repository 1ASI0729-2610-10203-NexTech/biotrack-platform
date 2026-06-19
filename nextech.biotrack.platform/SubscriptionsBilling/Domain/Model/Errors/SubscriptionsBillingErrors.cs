using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Errors;

public static class SubscriptionsBillingErrors
{
    public static readonly Error SubscriptionNotFound =
        new("SubscriptionsBilling.SubscriptionNotFound", "The specified subscription was not found.");

    public static readonly Error PlanNotFound =
        new("SubscriptionsBilling.PlanNotFound", "The specified subscription plan was not found.");

    public static readonly Error PaymentFailed =
        new("SubscriptionsBilling.PaymentFailed", "The payment could not be processed.");

    public static readonly Error PaymentRejected =
        new("SubscriptionsBilling.PaymentRejected", "The payment was rejected by the payment gateway.");

    public static readonly Error SubscriptionAlreadyActive =
        new("SubscriptionsBilling.SubscriptionAlreadyActive", "The subscription is already active.");

    public static readonly Error SubscriptionNotSuspended =
        new("SubscriptionsBilling.SubscriptionNotSuspended", "The subscription is not in a suspended state.");

    public static readonly Error SubscriptionNotEligibleForReactivation =
        new("SubscriptionsBilling.SubscriptionNotEligibleForReactivation",
            "The subscription is not eligible for reactivation.");

    public static readonly Error AccessDenied =
        new("SubscriptionsBilling.AccessDenied", "You do not have permission to access this subscription.");

    public static readonly Error InvalidStartDate =
        new("SubscriptionsBilling.InvalidStartDate", "The subscription start date is invalid.");

    public static readonly Error InvalidSubscriptionId =
        new("SubscriptionsBilling.InvalidSubscriptionId", "The subscription ID is invalid.");

    public static readonly Error DatabaseError =
        new("SubscriptionsBilling.DatabaseError", "A database error occurred.");

    public static readonly Error InternalServerError =
        new("SubscriptionsBilling.InternalServerError", "An unexpected error occurred.");
}
