namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record RegisterConsumptionResource(DateOnly Date, string MealType, string Description, int Calories);
