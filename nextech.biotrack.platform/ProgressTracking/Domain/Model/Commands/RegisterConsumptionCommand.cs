namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record RegisterConsumptionCommand(int PatientUserId, DateOnly Date, string MealType, string Description, int Calories);
