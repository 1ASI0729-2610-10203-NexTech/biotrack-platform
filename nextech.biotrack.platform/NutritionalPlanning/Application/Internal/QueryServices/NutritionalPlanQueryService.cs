using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.QueryServices;

public class NutritionalPlanQueryService(INutritionalPlanRepository planRepository) : INutritionalPlanQueryService
{
    public async Task<IEnumerable<NutritionalPlan>> Handle(GetNutritionalPlansByNutritionistIdQuery query, CancellationToken cancellationToken) =>
        await planRepository.FindByNutritionistIdAsync(query.NutritionistId, cancellationToken);

    public async Task<NutritionalPlan?> Handle(GetNutritionalPlanByIdQuery query, CancellationToken cancellationToken) =>
        await planRepository.FindByIdAsync(query.PlanId, cancellationToken);
}
