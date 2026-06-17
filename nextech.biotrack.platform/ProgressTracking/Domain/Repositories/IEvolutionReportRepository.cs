using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IEvolutionReportRepository : IBaseRepository<EvolutionReport>
{
    Task<IEnumerable<EvolutionReport>> FindByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken);
}
