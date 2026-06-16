namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

public record PatientProfileResource(
    int Id,
    int PatientUserId,
    decimal CurrentWeightKg,
    decimal HeightCm,
    int Age,
    string BiologicalSex,
    string ActivityLevel,
    int SystolicPressure,
    int DiastolicPressure,
    decimal BasalGlucoseMgDl,
    decimal Bmi,
    string BmiCategory,
    string? NutritionalGoal,
    string GoalStatus,
    bool IsComplete,
    IEnumerable<FoodRestrictionItemResource> FoodRestrictions,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
