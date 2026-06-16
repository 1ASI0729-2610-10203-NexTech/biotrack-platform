using Cortex.Mediator.Notifications;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Events;

namespace nextech.biotrack.platform.CorporateManagement.Application.Internal.EventHandlers;

public class CompanyCollaboratorsUploadedEventHandler : INotificationHandler<CompanyCollaboratorsUploadedEvent>
{
    public Task Handle(CompanyCollaboratorsUploadedEvent notification, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
