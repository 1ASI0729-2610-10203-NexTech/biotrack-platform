namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record UpdateWeightCommand(int PatientUserId, DateOnly Date, decimal WeightKg);
