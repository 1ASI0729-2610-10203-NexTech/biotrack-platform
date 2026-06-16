namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record AcceptPlanCommand(int PlanId, int PatientUserId);
