namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record ConsumptionRecordResource(int Id, int PatientUserId, DateOnly Date, string MealType, string Description, int Calories);
