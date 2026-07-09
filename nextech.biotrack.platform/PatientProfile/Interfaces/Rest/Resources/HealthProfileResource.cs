namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

public record HealthProfileResource(
    int Id,
    int UserId,
    decimal HeightCm,
    decimal WeightKg,
    decimal GoalWeightKg,
    decimal Bmi,
    string ActivityLevel,
    string NutritionalObjective,
    string DietaryRestrictions,
    int? Age,
    string? BiologicalSex,
    int? SystolicPressure,
    int? DiastolicPressure,
    decimal? GlucoseMgDl,
    DateTime UpdatedAt);

public record UpdateHealthDataResource(
    decimal HeightCm,
    decimal WeightKg,
    decimal GoalWeightKg,
    string ActivityLevel,
    string NutritionalObjective,
    int? Age = null,
    string? BiologicalSex = null,
    int? SystolicPressure = null,
    int? DiastolicPressure = null,
    decimal? GlucoseMgDl = null);

public record UpdateDietaryRestrictionsResource(string Restrictions);
