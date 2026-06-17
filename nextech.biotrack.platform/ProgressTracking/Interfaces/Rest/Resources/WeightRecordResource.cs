namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record WeightRecordResource(int Id, int PatientUserId, DateOnly Date, decimal WeightKg);
