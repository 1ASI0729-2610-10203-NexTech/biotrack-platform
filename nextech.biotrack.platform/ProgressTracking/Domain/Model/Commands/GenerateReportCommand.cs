namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record GenerateReportCommand(int PatientUserId, int RequestedByUserId, DateOnly PeriodStart, DateOnly PeriodEnd);
