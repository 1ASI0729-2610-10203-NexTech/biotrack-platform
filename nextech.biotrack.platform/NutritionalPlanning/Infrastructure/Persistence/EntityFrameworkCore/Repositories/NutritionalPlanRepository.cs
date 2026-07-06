using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class NutritionalPlanRepository(AppDbContext context)
    : BaseRepository<NutritionalPlan>(context), INutritionalPlanRepository
{
    public async Task<IEnumerable<NutritionalPlan>> FindByNutritionistIdAsync(int nutritionistId, CancellationToken cancellationToken) =>
        await Context.Set<NutritionalPlan>()
            .Where(p => p.NutritionistId == nutritionistId)
            .ToListAsync(cancellationToken);
}
