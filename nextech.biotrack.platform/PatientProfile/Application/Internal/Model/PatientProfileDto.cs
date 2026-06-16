namespace nextech.biotrack.platform.PatientProfile.Application.Internal.Model;

public record FoodRestrictionDto(string Type, string Description, string Severity);

public record PatientProfileDto(
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
    IEnumerable<FoodRestrictionDto> FoodRestrictions,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
