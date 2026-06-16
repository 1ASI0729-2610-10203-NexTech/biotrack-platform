using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.OutboundServices;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.OutboundServices;

public class EmailAdapter(ILogger<EmailAdapter> logger) : IEmailAdapter
{
    public Task<bool> SendAppointmentReminderAsync(
        string toEmail,
        string patientName,
        DateTime scheduledAt,
        string modality,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Sending appointment reminder to {Email} — Patient: {Name}, Date: {Date}, Modality: {Modality}",
            toEmail, patientName, scheduledAt, modality);

        return Task.FromResult(true);
    }
}
