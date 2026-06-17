using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;
using nextech.biotrack.platform.ProgressTracking.Application.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.QueryServices;

public class ProgressChartQueryService(
    IWeightRecordRepository weightRepository,
    IWeeklyAdherenceRepository adherenceRepository)
    : IProgressChartQueryService
{
    public async Task<ProgressChartResult?> Handle(GetProgressChartQuery query, CancellationToken cancellationToken)
    {
        var weights = (await weightRepository.FindByPatientUserIdAndDateRangeAsync(
            query.PatientUserId, query.PeriodStart, query.PeriodEnd, cancellationToken))
            .OrderBy(w => w.Date).ToList();

        var adherences = (await adherenceRepository.FindByPatientUserIdAndDateRangeAsync(
            query.PatientUserId, query.PeriodStart, query.PeriodEnd, cancellationToken))
            .OrderBy(a => a.WeekStart).ToList();

        if (!weights.Any() && !adherences.Any()) return null;

        var weightHistory = weights.Select((w, i) => new WeightDataPoint($"Week {i + 1}", w.WeightKg)).ToList();
        var adherenceHistory = adherences.Select(a => new AdherenceDataPoint(a.WeekStart, a.AdherencePct)).ToList();
        var overallChange = weights.Count >= 2 ? weights.Last().WeightKg - weights.First().WeightKg : 0m;
        var avgAdherence = adherences.Any() ? adherences.Average(a => a.AdherencePct) : 0m;

        return new ProgressChartResult(query.PatientUserId, weightHistory, adherenceHistory, overallChange, null, avgAdherence);
    }
}
