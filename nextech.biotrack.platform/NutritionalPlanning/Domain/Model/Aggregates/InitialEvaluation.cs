using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

public partial class InitialEvaluation
{
    public InitialEvaluation() : this(0, 0, string.Empty, 0, 0, 0, 0)
    {
    }

    public InitialEvaluation(int patientProfileId, int nutritionistUserId, string observations,
        int caloriesTarget, int proteinsPct, int carbohydratesPct, int fatsPct)
    {
        PatientProfileId = patientProfileId;
        NutritionistUserId = nutritionistUserId;
        Observations = observations;
        CaloriesTarget = caloriesTarget;
        ProteinsPct = proteinsPct;
        CarbohydratesPct = carbohydratesPct;
        FatsPct = fatsPct;
        Status = EvaluationStatus.Completed.ToString();
    }

    public int Id { get; }
    public int PatientProfileId { get; private set; }
    public int NutritionistUserId { get; private set; }
    public string Observations { get; private set; }
    public int CaloriesTarget { get; private set; }
    public int ProteinsPct { get; private set; }
    public int CarbohydratesPct { get; private set; }
    public int FatsPct { get; private set; }
    public string Status { get; private set; }
}
