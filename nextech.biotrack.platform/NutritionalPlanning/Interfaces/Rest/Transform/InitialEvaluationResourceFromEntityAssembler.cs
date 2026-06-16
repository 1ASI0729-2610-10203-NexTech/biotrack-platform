using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class InitialEvaluationResourceFromEntityAssembler
{
    public static InitialEvaluationResource ToResourceFromEntity(InitialEvaluation evaluation) =>
        new(evaluation.Id, evaluation.PatientProfileId, evaluation.NutritionistUserId,
            evaluation.Observations, evaluation.CaloriesTarget,
            evaluation.ProteinsPct, evaluation.CarbohydratesPct, evaluation.FatsPct,
            evaluation.Status, evaluation.CreatedAt, evaluation.UpdatedAt);
}
