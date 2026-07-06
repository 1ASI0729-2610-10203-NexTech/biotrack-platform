namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;

public record UpdateHealthDataCommand(
    int UserId,
    decimal HeightCm,
    decimal WeightKg,
    decimal GoalWeightKg,
    string ActivityLevel,
    string NutritionalObjective);

public record UpdateDietaryRestrictionsCommand(int UserId, string Restrictions);
