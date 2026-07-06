using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Domain.Repositories;

public interface IHealthProfileRepository : IBaseRepository<HealthProfile>
{
    Task<HealthProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken);
}
