namespace nextech.biotrack.platform.SubscriptionsBilling.Interfaces.Rest.Resources;

public record InvoiceResource(
    int Id,
    int SubscriptionId,
    DateOnly IssuedDate,
    DateOnly DueDate,
    decimal Amount,
    bool IsPaid);
