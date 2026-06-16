namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

public record CompleteEvaluationResource(
    string Observations,
    int CaloriesTarget,
    int ProteinsPct,
    int CarbohydratesPct,
    int FatsPct);
