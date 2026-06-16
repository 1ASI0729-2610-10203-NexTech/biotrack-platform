using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.Model;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Queries;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;

public interface INutritionalPlanQueryService
{
    Task<WeeklyDietDto?> Handle(GetWeeklyDietQuery query, CancellationToken cancellationToken);
}
