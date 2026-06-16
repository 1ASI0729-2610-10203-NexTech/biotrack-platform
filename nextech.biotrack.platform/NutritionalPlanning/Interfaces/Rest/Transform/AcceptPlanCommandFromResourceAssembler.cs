using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class AcceptPlanCommandFromResourceAssembler
{
    public static AcceptPlanCommand ToCommandFromResource(int planId, int patientUserId) =>
        new(planId, patientUserId);
}
