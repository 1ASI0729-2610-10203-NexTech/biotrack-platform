using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IActivityRecordRepository : IBaseRepository<ActivityRecord>
{
    Task<IEnumerable<ActivityRecord>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken);

    Task<IEnumerable<ActivityRecord>> FindByPatientUserIdAndWeekAsync(
        int patientUserId, DateOnly weekStart, CancellationToken cancellationToken);
}
