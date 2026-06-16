using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.OutboundServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;

public class ControlAppointmentCommandService(
    IControlAppointmentRepository appointmentRepository,
    IEmailAdapter emailAdapter,
    IUnitOfWork unitOfWork)
    : IControlAppointmentCommandService
{
    public async Task<Result<ControlAppointment>> Handle(
        ScheduleAppointmentCommand command, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<AppointmentModality>(command.Modality, ignoreCase: true, out _))
            return Result<ControlAppointment>.Failure(NutritionalPlanningError.InvalidAppointmentModality,
                $"'{command.Modality}' is not a valid modality. Use InPerson, VideoCall or Phone.");

        if (command.ScheduledAt <= DateTime.UtcNow)
            return Result<ControlAppointment>.Failure(NutritionalPlanningError.InvalidAppointmentDate,
                "The appointment date must be in the future.");

        var appointment = new ControlAppointment(
            command.PatientUserId, command.NutritionistUserId, command.ScheduledAt, command.Modality);

        try
        {
            await appointmentRepository.AddAsync(appointment, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ControlAppointment>.Success(appointment);
        }
        catch (OperationCanceledException)
        {
            return Result<ControlAppointment>.Failure(NutritionalPlanningError.OperationCancelled,
                "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<ControlAppointment>.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while saving the appointment.");
        }
        catch (Exception)
        {
            return Result<ControlAppointment>.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    public async Task<Result> Handle(SendReminderCommand command, CancellationToken cancellationToken)
    {
        var upcoming = (await appointmentRepository.FindScheduledForReminderAsync(cancellationToken)).ToList();

        foreach (var appointment in upcoming)
        {
            try
            {
                var sent = await emailAdapter.SendAppointmentReminderAsync(
                    toEmail: $"patient_{appointment.PatientUserId}@biotrack.com",
                    patientName: $"Patient {appointment.PatientUserId}",
                    scheduledAt: appointment.ScheduledAt,
                    modality: appointment.Modality,
                    cancellationToken: cancellationToken);

                if (sent) appointment.MarkReminderSent();
                else appointment.MarkReminderError();
            }
            catch
            {
                appointment.MarkReminderError();
            }

            appointmentRepository.Update(appointment);
        }

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(NutritionalPlanningError.DatabaseError,
                "A database error occurred while updating reminders.");
        }
        catch (Exception)
        {
            return Result.Failure(NutritionalPlanningError.InternalServerError,
                "An unexpected error occurred.");
        }
    }
}
