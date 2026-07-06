namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

public class NutritionalPlan
{
    public NutritionalPlan() : this(string.Empty, 0, 0, 0, 0, 0) { }

    public NutritionalPlan(string name, int calorieTarget, int proteinGrams, int carbsGrams, int fatGrams, int nutritionistId)
    {
        Name = name;
        CalorieTarget = calorieTarget;
        ProteinGrams = proteinGrams;
        CarbsGrams = carbsGrams;
        FatGrams = fatGrams;
        NutritionistId = nutritionistId;
        Status = "Proposed";
    }

    public int Id { get; }
    public string Name { get; private set; }
    public int CalorieTarget { get; private set; }
    public int ProteinGrams { get; private set; }
    public int CarbsGrams { get; private set; }
    public int FatGrams { get; private set; }
    public int NutritionistId { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Activate() => Status = "Active";
    public void Complete() => Status = "Completed";
}
