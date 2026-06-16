using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Events;

public record AppointmentScheduledEvent(int AppointmentId, int PatientUserId, DateTime ScheduledAt) : IEvent;
