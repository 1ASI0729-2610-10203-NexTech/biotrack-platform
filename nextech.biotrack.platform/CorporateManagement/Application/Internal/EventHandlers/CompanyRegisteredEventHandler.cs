using Cortex.Mediator.Notifications;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Events;

namespace nextech.biotrack.platform.CorporateManagement.Application.Internal.EventHandlers;

public class CompanyRegisteredEventHandler : INotificationHandler<CompanyRegisteredEvent>
{
    public Task Handle(CompanyRegisteredEvent notification, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
