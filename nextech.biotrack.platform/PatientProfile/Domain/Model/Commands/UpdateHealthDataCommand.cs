namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;

public record UpdateHealthDataCommand(
    int UserId,
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

public record UpdateDietaryRestrictionsCommand(int UserId, string Restrictions);
