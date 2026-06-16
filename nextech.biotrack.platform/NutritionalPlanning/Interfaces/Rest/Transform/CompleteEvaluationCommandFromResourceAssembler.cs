using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class CompleteEvaluationCommandFromResourceAssembler
{
    public static CompleteEvaluationCommand ToCommandFromResource(
        int patientProfileId, int nutritionistUserId, CompleteEvaluationResource resource) =>
        new(patientProfileId, nutritionistUserId, resource.Observations,
            resource.CaloriesTarget, resource.ProteinsPct, resource.CarbohydratesPct, resource.FatsPct);
}
