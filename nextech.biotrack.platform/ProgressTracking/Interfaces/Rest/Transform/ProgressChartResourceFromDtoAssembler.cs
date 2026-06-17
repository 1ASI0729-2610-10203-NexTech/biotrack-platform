using nextech.biotrack.platform.ProgressTracking.Application.Dtos;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class ProgressChartResourceFromDtoAssembler
{
    public static ProgressChartResource ToResourceFromDto(ProgressChartDto dto)
    {
        var weightHistory = dto.WeightHistory.Select(w => new WeightDataPointResource(w.DayLabel, w.WeightKg));
        var adherenceHistory = dto.AdherenceHistory.Select(a => new AdherenceDataPointResource(a.WeekStart, a.AdherencePct));
        return new ProgressChartResource(dto.PatientUserId, weightHistory, adherenceHistory,
            dto.OverallChangeWeight, dto.TargetWeight, dto.AverageAdherencePct);
    }
}
