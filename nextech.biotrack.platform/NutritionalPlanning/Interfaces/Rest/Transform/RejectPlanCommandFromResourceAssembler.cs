using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class RejectPlanCommandFromResourceAssembler
{
    public static RejectPlanCommand ToCommandFromResource(int planId, int patientUserId, RejectPlanResource resource) =>
        new(planId, patientUserId, resource.RejectionNotes);
}
