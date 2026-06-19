using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;

public class Invoice : IAuditableEntity
{
    public Invoice() : this(0, DateOnly.MinValue, DateOnly.MinValue, 0m)
    {
    }

    public Invoice(int subscriptionId, DateOnly issuedDate, DateOnly dueDate, decimal amount)
    {
        SubscriptionId = subscriptionId;
        IssuedDate = issuedDate;
        DueDate = dueDate;
        Amount = amount;
        IsPaid = false;
    }

    public int Id { get; private set; }
    public int SubscriptionId { get; private set; }
    public DateOnly IssuedDate { get; private set; }
    public DateOnly DueDate { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Invoice MarkPaid()
    {
        IsPaid = true;
        return this;
    }
}
