using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Events;

public record MetricsReadyForConsolidationEvent(int PatientUserId, DateOnly WeekStart) : IEvent;
