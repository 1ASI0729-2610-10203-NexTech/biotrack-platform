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
    DateTime UpdatedAt);

public record UpdateHealthDataResource(
    decimal HeightCm,
    decimal WeightKg,
    decimal GoalWeightKg,
    string ActivityLevel,
    string NutritionalObjective);

public record UpdateDietaryRestrictionsResource(string Restrictions);
