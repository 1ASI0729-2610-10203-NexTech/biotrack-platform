using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Application.Internal.OutboundServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Model;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;

public class AdherenceCommandService(
    IWeeklyAdherenceRepository adherenceRepository,
    IAdherenceCalculatorService calculatorService,
    IUnitOfWork unitOfWork)
    : IAdherenceCommandService
{
    public async Task<Result<WeeklyAdherence>> Handle(CalculateAdherenceCommand command, CancellationToken cancellationToken)
    {
        decimal adherencePct;
        try
        {
            adherencePct = await calculatorService.CalculateAsync(
                command.PatientUserId, command.PlanId, command.WeekStart, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Result<WeeklyAdherence>.Failure(ProgressTrackingError.InsufficientProgressData, ex.Message);
        }

        var existing = await adherenceRepository.FindByPatientAndWeekAsync(
            command.PatientUserId, command.PlanId, command.WeekStart, cancellationToken);

        try
        {
            WeeklyAdherence record;
            if (existing != null)
            {
                existing.UpdateAdherence(adherencePct);
                adherenceRepository.Update(existing);
                record = existing;
            }
            else
            {
                record = new WeeklyAdherence(command.PatientUserId, command.PlanId, command.WeekStart, adherencePct);
                await adherenceRepository.AddAsync(record, cancellationToken);
            }

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<WeeklyAdherence>.Success(record);
        }
        catch (OperationCanceledException)
        {
            return Result<WeeklyAdherence>.Failure(ProgressTrackingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<WeeklyAdherence>.Failure(ProgressTrackingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<WeeklyAdherence>.Failure(ProgressTrackingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
