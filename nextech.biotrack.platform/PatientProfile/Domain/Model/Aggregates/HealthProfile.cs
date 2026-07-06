namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;

public class HealthProfile
{
    public HealthProfile() : this(0, 0m, 0m, 0m, string.Empty, string.Empty, string.Empty) { }

    public HealthProfile(int userId, decimal heightCm, decimal weightKg, decimal goalWeightKg,
        string activityLevel, string nutritionalObjective, string dietaryRestrictions)
    {
        UserId = userId;
        HeightCm = heightCm;
        WeightKg = weightKg;
        GoalWeightKg = goalWeightKg;
        ActivityLevel = activityLevel;
        NutritionalObjective = nutritionalObjective;
        DietaryRestrictions = dietaryRestrictions;
    }

    public int Id { get; }
    public int UserId { get; private set; }
    public decimal HeightCm { get; private set; }
    public decimal WeightKg { get; private set; }
    public decimal GoalWeightKg { get; private set; }
    public string ActivityLevel { get; private set; }
    public string NutritionalObjective { get; private set; }
    public string DietaryRestrictions { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public decimal Bmi => HeightCm > 0 ? Math.Round(WeightKg / ((HeightCm / 100) * (HeightCm / 100)), 1) : 0;

    public int CalorieTarget()
    {
        decimal bmr = 10 * WeightKg + 6.25m * HeightCm - 300;
        decimal multiplier = ActivityLevel switch
        {
            "Sedentary" => 1.2m,
            "Light" => 1.375m,
            "Moderate" => 1.55m,
            "Active" => 1.725m,
            _ => 1.375m
        };
        return (int)(bmr * multiplier);
    }

    public int ProteinTarget() => (int)(WeightKg * 1.2m);
    public int CarbsTarget() => (int)(CalorieTarget() * 0.45m / 4);
    public int FatTarget() => (int)(CalorieTarget() * 0.25m / 9);

    public void UpdateHealthData(decimal heightCm, decimal weightKg, decimal goalWeightKg,
        string activityLevel, string nutritionalObjective)
    {
        HeightCm = heightCm;
        WeightKg = weightKg;
        GoalWeightKg = goalWeightKg;
        ActivityLevel = activityLevel;
        NutritionalObjective = nutritionalObjective;
    }

    public void UpdateDietaryRestrictions(string restrictions) => DietaryRestrictions = restrictions;
}
