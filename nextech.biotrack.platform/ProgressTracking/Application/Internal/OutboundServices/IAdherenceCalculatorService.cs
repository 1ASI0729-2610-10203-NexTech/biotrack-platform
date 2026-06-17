namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;

public interface IAdherenceCalculatorService
{
    Task<decimal> CalculateAsync(int patientUserId, int planId, DateOnly weekStart, CancellationToken cancellationToken);
}
