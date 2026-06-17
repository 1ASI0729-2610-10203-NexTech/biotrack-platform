using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IWeightRecordRepository : IBaseRepository<WeightRecord>
{
    Task<WeightRecord?> FindByPatientUserIdAndDateAsync(
        int patientUserId, DateOnly date, CancellationToken cancellationToken);

    Task<IEnumerable<WeightRecord>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken);
}
