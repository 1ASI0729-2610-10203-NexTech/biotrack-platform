using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

public interface IWeeklyAdherenceRepository : IBaseRepository<WeeklyAdherence>
{
    Task<WeeklyAdherence?> FindByPatientAndWeekAsync(
        int patientUserId, int planId, DateOnly weekStart, CancellationToken cancellationToken);

    Task<IEnumerable<WeeklyAdherence>> FindByPatientUserIdAndDateRangeAsync(
        int patientUserId, DateOnly start, DateOnly end, CancellationToken cancellationToken);
}
