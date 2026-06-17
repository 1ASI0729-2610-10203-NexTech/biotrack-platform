using nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Services;

public class NutritionalPlanAdapter : INutritionalPlanAdapter
{
    // Placeholder until NutritionalPlanning BC is integrated
    public Task<int?> GetExpectedDailyCaloriesAsync(int planId, CancellationToken cancellationToken)
    {
        return Task.FromResult<int?>(null);
    }
}
