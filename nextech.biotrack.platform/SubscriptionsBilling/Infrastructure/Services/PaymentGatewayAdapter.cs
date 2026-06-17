using nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.OutboundServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Services;

public class PaymentGatewayAdapter : IPaymentGatewayAdapter
{
    public Task<PaymentResult> ProcessPaymentAsync(int subscriptionId, decimal amount, CancellationToken cancellationToken)
    {
        var transactionId = Guid.NewGuid().ToString("N");
        var result = new PaymentResult(true, transactionId, "Payment approved by gateway.");
        return Task.FromResult(result);
    }
}
