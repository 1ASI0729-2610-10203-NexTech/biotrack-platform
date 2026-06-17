namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;

public interface INutritionalPlanAdapter
{
    Task<int?> GetExpectedDailyCaloriesAsync(int planId, CancellationToken cancellationToken);
}
