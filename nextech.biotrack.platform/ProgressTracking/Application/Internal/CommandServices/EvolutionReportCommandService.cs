using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.ValueObjects;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;

public class EvolutionReportCommandService(
    IEvolutionReportRepository reportRepository,
    IConsumptionRecordRepository consumptionRepository,
    IActivityRecordRepository activityRepository,
    IWeightRecordRepository weightRepository,
    IWeeklyAdherenceRepository adherenceRepository,
    IPdfServiceAdapter pdfService,
    IUnitOfWork unitOfWork)
    : IEvolutionReportCommandService
{
    public async Task<Result<EvolutionReport>> Handle(GenerateReportCommand command, CancellationToken cancellationToken)
    {
        var consumptions = (await consumptionRepository.FindByPatientUserIdAndDateRangeAsync(
            command.PatientUserId, command.PeriodStart, command.PeriodEnd, cancellationToken)).ToList();

        var activities = (await activityRepository.FindByPatientUserIdAndDateRangeAsync(
            command.PatientUserId, command.PeriodStart, command.PeriodEnd, cancellationToken)).ToList();

        var weights = (await weightRepository.FindByPatientUserIdAndDateRangeAsync(
            command.PatientUserId, command.PeriodStart, command.PeriodEnd, cancellationToken)).ToList();

        var adherences = (await adherenceRepository.FindByPatientUserIdAndDateRangeAsync(
            command.PatientUserId, command.PeriodStart, command.PeriodEnd, cancellationToken)).ToList();

        var report = new EvolutionReport(command.PatientUserId, command.RequestedByUserId, command.PeriodStart, command.PeriodEnd);

        if (!consumptions.Any() && !weights.Any())
        {
            report.SetInsufficientData();
            try
            {
                await reportRepository.AddAsync(report, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
            }
            catch { /* best-effort save */ }
            return Result<EvolutionReport>.Failure(ProgressTrackingError.InsufficientProgressData,
                "Not enough data available to generate the evolution report.");
        }

        try
        {
            var avgCalories = consumptions.Any() ? (decimal)consumptions.Average(c => c.Calories) : 0m;
            var avgWeight = weights.Any() ? weights.Average(w => w.WeightKg) : 0m;
            var avgAdherence = adherences.Any() ? adherences.Average(a => a.AdherencePct) : 0m;
            var totalActivityMin = activities.Sum(a => a.DurationMinutes);

            var metrics = new PatientMetricData(
                command.PatientUserId, command.PeriodStart, command.PeriodEnd,
                avgCalories, avgWeight, avgAdherence, totalActivityMin);

            var pdfBytes = await pdfService.GenerateEvolutionReportAsync(metrics, cancellationToken);
            report.SetGenerated(pdfBytes);

            await reportRepository.AddAsync(report, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<EvolutionReport>.Success(report);
        }
        catch (OperationCanceledException)
        {
            return Result<EvolutionReport>.Failure(ProgressTrackingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<EvolutionReport>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            report.SetError();
            return Result<EvolutionReport>.Failure(ProgressTrackingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
