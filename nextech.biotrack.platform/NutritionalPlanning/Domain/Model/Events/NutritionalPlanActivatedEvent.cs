using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Events;

public record NutritionalPlanActivatedEvent(int PlanId, int PatientUserId) : IEvent;
