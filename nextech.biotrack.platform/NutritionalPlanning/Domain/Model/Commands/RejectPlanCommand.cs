namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record RejectPlanCommand(int PlanId, int PatientUserId, string RejectionNotes);
