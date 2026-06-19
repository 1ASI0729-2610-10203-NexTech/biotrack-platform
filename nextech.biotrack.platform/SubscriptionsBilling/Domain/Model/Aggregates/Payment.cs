using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;

public partial class Payment
{
    public Payment() : this(0, DateOnly.MinValue, 0m)
    {
    }

    public Payment(int subscriptionId, DateOnly paymentDate, decimal amount)
    {
        SubscriptionId = subscriptionId;
        PaymentDate = paymentDate;
        Amount = amount;
        Status = PaymentStatus.Pending;
        TransactionId = string.Empty;
        GatewayMessage = string.Empty;
    }

    public int Id { get; private set; }
    public int SubscriptionId { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string TransactionId { get; private set; }
    public string GatewayMessage { get; private set; }

    public Payment Approve(string transactionId, string gatewayMessage)
    {
        Status = PaymentStatus.Approved;
        TransactionId = transactionId;
        GatewayMessage = gatewayMessage;
        return this;
    }

    public Payment Reject(string transactionId, string gatewayMessage)
    {
        Status = PaymentStatus.Rejected;
        TransactionId = transactionId;
        GatewayMessage = gatewayMessage;
        return this;
    }
}
