namespace nextech.biotrack.platform.NutritionalPlanning.Application.Internal.OutboundServices;

public interface IEmailAdapter
{
    Task<bool> SendAppointmentReminderAsync(
        string toEmail,
        string patientName,
        DateTime scheduledAt,
        string modality,
        CancellationToken cancellationToken);
}
