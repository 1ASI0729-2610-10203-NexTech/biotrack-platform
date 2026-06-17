using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Events;

public record LowAdherenceAlertSentEvent(
    int PatientUserId,
    int PlanId,
    DateOnly WeekStart,
    decimal AdherencePct) : IEvent;
