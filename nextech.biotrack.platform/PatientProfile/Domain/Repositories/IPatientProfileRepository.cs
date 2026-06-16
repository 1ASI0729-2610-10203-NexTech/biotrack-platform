using nextech.biotrack.platform.Shared.Domain.Repositories;
using HealthProfile = nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates.PatientProfile;

namespace nextech.biotrack.platform.PatientProfile.Domain.Repositories;

public interface IPatientProfileRepository : IBaseRepository<HealthProfile>
{
    Task<HealthProfile?> FindByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
    Task<HealthProfile?> FindByPatientUserIdWithRestrictionsAsync(int patientUserId, CancellationToken cancellationToken);
    Task<bool> ExistsByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
}
