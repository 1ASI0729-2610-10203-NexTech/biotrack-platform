namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;

public enum AppointmentStatus
{
    Scheduled,
    ReminderSent,
    ReminderError,
    Completed,
    Cancelled,
    NoShow
}
