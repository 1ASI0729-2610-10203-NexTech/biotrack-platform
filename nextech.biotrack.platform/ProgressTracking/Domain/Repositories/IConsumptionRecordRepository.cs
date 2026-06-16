using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IConsumptionRecordRepository : IBaseRepository<ConsumptionRecord>
{
    Task<IEnumerable<ConsumptionRecord>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken);

    Task<IEnumerable<ConsumptionRecord>> FindByPatientUserIdAndWeekAsync(
        int patientUserId, DateOnly weekStart, CancellationToken cancellationToken);
}
