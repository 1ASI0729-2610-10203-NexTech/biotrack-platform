using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;

public interface IPdfServiceAdapter
{
    Task<byte[]> GenerateEvolutionReportAsync(PatientMetricData metrics, CancellationToken cancellationToken);
}
