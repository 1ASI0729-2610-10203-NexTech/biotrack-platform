using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;

public interface IInitialEvaluationRepository : IBaseRepository<InitialEvaluation>
{
    Task<InitialEvaluation?> FindByPatientProfileIdAsync(int patientProfileId, CancellationToken cancellationToken);
    Task<bool> ExistsByPatientProfileIdAsync(int patientProfileId, CancellationToken cancellationToken);
}
