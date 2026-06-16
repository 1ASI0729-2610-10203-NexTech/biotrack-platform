using nextech.biotrack.platform.PatientProfile.Domain.Model.Entities;
using nextech.biotrack.platform.PatientProfile.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;

public partial class PatientProfile
{
    public PatientProfile() : this(0, 0, 0, 0, string.Empty, string.Empty, 0, 0, 0) { }

    public PatientProfile(
        int patientUserId,
        decimal currentWeightKg,
        decimal heightCm,
        int age,
        string biologicalSex,
        string activityLevel,
        int systolicPressure,
        int diastolicPressure,
        decimal basalGlucoseMgDl)
    {
        PatientUserId = patientUserId;
        CurrentWeightKg = currentWeightKg;
        HeightCm = heightCm;
        Age = age;
        BiologicalSex = biologicalSex;
        ActivityLevel = activityLevel;
        SystolicPressure = systolicPressure;
        DiastolicPressure = diastolicPressure;
        BasalGlucoseMgDl = basalGlucoseMgDl;
        Bmi = Imc.Calculate(currentWeightKg, heightCm).Value;
        GoalStatus = ValueObjects.GoalStatus.NotDefined.ToString();
        IsComplete = false;
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public decimal CurrentWeightKg { get; private set; }
    public decimal HeightCm { get; private set; }
    public int Age { get; private set; }
    public string BiologicalSex { get; private set; }
    public string ActivityLevel { get; private set; }
    public int SystolicPressure { get; private set; }
    public int DiastolicPressure { get; private set; }
    public decimal BasalGlucoseMgDl { get; private set; }
    public decimal Bmi { get; private set; }
    public string BmiCategory => Imc.Calculate(CurrentWeightKg, HeightCm).Category;
    public string? NutritionalGoal { get; private set; }
    public string GoalStatus { get; private set; }
    public bool IsComplete { get; private set; }

    public ICollection<FoodRestriction> FoodRestrictions { get; private set; } = new List<FoodRestriction>();

    public void DefineGoal(string goal)
    {
        NutritionalGoal = goal;
        GoalStatus = ValueObjects.GoalStatus.InProgress.ToString();
        IsComplete = true;
    }

    public void ReplaceRestrictions(IEnumerable<(string type, string description, string severity)> restrictionData)
    {
        FoodRestrictions.Clear();
        foreach (var (type, description, severity) in restrictionData)
            FoodRestrictions.Add(new FoodRestriction(Id, type, description, severity));
    }
}
