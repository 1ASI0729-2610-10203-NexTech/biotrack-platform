using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using HealthProfile = nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates.PatientProfile;

namespace nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PatientProfileRepository(AppDbContext context)
    : BaseRepository<HealthProfile>(context), IPatientProfileRepository
{
    public async Task<HealthProfile?> FindByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken) =>
        await Context.Set<HealthProfile>()
            .FirstOrDefaultAsync(p => p.PatientUserId == patientUserId, cancellationToken);

    public async Task<HealthProfile?> FindByPatientUserIdWithRestrictionsAsync(
        int patientUserId, CancellationToken cancellationToken) =>
        await Context.Set<HealthProfile>()
            .Include(p => p.FoodRestrictions)
            .FirstOrDefaultAsync(p => p.PatientUserId == patientUserId, cancellationToken);

    public async Task<bool> ExistsByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken) =>
        await Context.Set<HealthProfile>()
            .AnyAsync(p => p.PatientUserId == patientUserId, cancellationToken);
}
