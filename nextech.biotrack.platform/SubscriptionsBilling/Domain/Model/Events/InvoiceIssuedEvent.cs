namespace nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Events;

public record InvoiceIssuedEvent(int InvoiceId, int SubscriptionId, decimal Amount, DateOnly DueDate);
