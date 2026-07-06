using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;

public interface INutritionalPlanQueryService
{
    Task<IEnumerable<NutritionalPlan>> Handle(GetNutritionalPlansByNutritionistIdQuery query, CancellationToken cancellationToken);
    Task<NutritionalPlan?> Handle(GetNutritionalPlanByIdQuery query, CancellationToken cancellationToken);
}
