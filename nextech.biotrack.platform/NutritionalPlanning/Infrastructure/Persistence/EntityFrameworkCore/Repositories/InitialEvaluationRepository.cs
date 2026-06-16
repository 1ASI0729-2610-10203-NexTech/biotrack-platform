using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InitialEvaluationRepository(AppDbContext context)
    : BaseRepository<InitialEvaluation>(context), IInitialEvaluationRepository
{
    public async Task<InitialEvaluation?> FindByPatientProfileIdAsync(
        int patientProfileId, CancellationToken cancellationToken)
    {
        return await Context.Set<InitialEvaluation>()
            .FirstOrDefaultAsync(e => e.PatientProfileId == patientProfileId, cancellationToken);
    }

    public async Task<bool> ExistsByPatientProfileIdAsync(
        int patientProfileId, CancellationToken cancellationToken)
    {
        return await Context.Set<InitialEvaluation>()
            .AnyAsync(e => e.PatientProfileId == patientProfileId, cancellationToken);
    }
}
