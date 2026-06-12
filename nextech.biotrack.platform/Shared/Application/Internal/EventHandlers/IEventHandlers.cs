using nextech.biotrack.platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;
namespace nextech.biotrack.platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}