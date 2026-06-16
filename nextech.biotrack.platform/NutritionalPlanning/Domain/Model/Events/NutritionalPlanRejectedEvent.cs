using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Events;

public record NutritionalPlanRejectedEvent(int PlanId, int PatientUserId, string RejectionNotes) : IEvent;
