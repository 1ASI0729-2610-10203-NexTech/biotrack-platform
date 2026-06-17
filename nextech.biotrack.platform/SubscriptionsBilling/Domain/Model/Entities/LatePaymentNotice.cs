using nextech.biotrack.platform.Shared.Domain.Model.Entities;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;

public class LatePaymentNotice : IAuditableEntity
{
    public LatePaymentNotice() : this(0, DateOnly.MinValue)
    {
    }

    public LatePaymentNotice(int subscriptionId, DateOnly noticeDate)
    {
        SubscriptionId = subscriptionId;
        NoticeDate = noticeDate;
        FailedAttempts = 1;
        Status = NoticeStatus.Issued;
    }

    public int Id { get; private set; }
    public int SubscriptionId { get; private set; }
    public DateOnly NoticeDate { get; private set; }
    public int FailedAttempts { get; private set; }
    public NoticeStatus Status { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public LatePaymentNotice IncrementFailedAttempts()
    {
        FailedAttempts++;
        if (FailedAttempts >= 3)
            Status = NoticeStatus.Escalated;
        return this;
    }

    public LatePaymentNotice Resolve()
    {
        Status = NoticeStatus.Resolved;
        return this;
    }
}
