using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class NutritionalPlanRepository(AppDbContext context)
    : BaseRepository<NutritionalPlan>(context), INutritionalPlanRepository
{
    public async Task<NutritionalPlan?> FindActivePlanByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken)
    {
        return await Context.Set<NutritionalPlan>()
            .Include(p => p.Days)
            .ThenInclude(d => d.Meals)
            .FirstOrDefaultAsync(
                p => p.PatientUserId == patientUserId
                     && p.Status == PlanStatus.Activated.ToString(),
                cancellationToken);
    }

    public async Task<NutritionalPlan?> FindByIdWithDaysAsync(int planId, CancellationToken cancellationToken)
    {
        return await Context.Set<NutritionalPlan>()
            .Include(p => p.Days)
            .ThenInclude(d => d.Meals)
            .FirstOrDefaultAsync(p => p.Id == planId, cancellationToken);
    }
}
