using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Domain.Repositories;

public interface IPatientProfileRepository : IBaseRepository<PatientProfile>
{
    Task<PatientProfile?> FindByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
    Task<PatientProfile?> FindByPatientUserIdWithRestrictionsAsync(int patientUserId, CancellationToken cancellationToken);
    Task<bool> ExistsByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
}
