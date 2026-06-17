namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;

public record GetProgressChartQuery(int PatientUserId, DateOnly PeriodStart, DateOnly PeriodEnd);
