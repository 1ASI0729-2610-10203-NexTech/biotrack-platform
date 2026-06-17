using nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Services;

public class AdherenceCalculatorService(
    IConsumptionRecordRepository consumptionRepository,
    INutritionalPlanAdapter nutritionalPlanAdapter)
    : IAdherenceCalculatorService
{
    public async Task<decimal> CalculateAsync(int patientUserId, int planId, DateOnly weekStart, CancellationToken cancellationToken)
    {
        var consumptions = (await consumptionRepository.FindByPatientUserIdAndWeekAsync(
            patientUserId, weekStart, cancellationToken)).ToList();

        if (!consumptions.Any())
            throw new InvalidOperationException("No consumption records found for the specified week.");

        var expectedDailyCalories = await nutritionalPlanAdapter.GetExpectedDailyCaloriesAsync(planId, cancellationToken);

        if (!expectedDailyCalories.HasValue || expectedDailyCalories.Value <= 0)
            return Math.Min(100m, Math.Round((decimal)consumptions.Average(c => c.Calories) / 2000m * 100m, 2));

        var actualTotal = consumptions.Sum(c => c.Calories);
        var expectedTotal = expectedDailyCalories.Value * 7;

        return Math.Min(100m, Math.Round((decimal)actualTotal / expectedTotal * 100m, 2));
    }
}
