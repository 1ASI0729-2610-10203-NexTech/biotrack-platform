using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Events;

public record PatientProfileCompletedEvent(int PatientProfileId, int PatientUserId) : IEvent;
