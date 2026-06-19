using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.OutboundServices;

public interface IPaymentGatewayAdapter
{
    Task<PaymentResult> ProcessPaymentAsync(int subscriptionId, decimal amount, CancellationToken cancellationToken);
}
