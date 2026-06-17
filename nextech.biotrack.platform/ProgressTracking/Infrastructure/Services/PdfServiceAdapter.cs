using System.Text;
using nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.ProgressTracking.Infrastructure.Services;

public class PdfServiceAdapter : IPdfServiceAdapter
{
    public Task<byte[]> GenerateEvolutionReportAsync(PatientMetricData metrics, CancellationToken cancellationToken)
    {
        var content = new StringBuilder();
        content.AppendLine("BioTrack Evolution Report");
        content.AppendLine($"Patient ID: {metrics.PatientUserId}");
        content.AppendLine($"Period: {metrics.PeriodStart} - {metrics.PeriodEnd}");
        content.AppendLine($"Average Weight: {metrics.AverageWeightKg:F1} kg");
        content.AppendLine($"Average Daily Calories: {metrics.AverageCalories:F0} kcal");
        content.AppendLine($"Average Adherence: {metrics.AverageAdherencePct:F1}%");
        content.AppendLine($"Total Activity: {metrics.TotalActivityMinutes} min");
        return Task.FromResult(Encoding.UTF8.GetBytes(content.ToString()));
    }
}
