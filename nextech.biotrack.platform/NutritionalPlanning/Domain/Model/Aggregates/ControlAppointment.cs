using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

public partial class ControlAppointment
{
    public ControlAppointment() : this(0, null, DateTime.UtcNow, string.Empty)
    {
    }

    public ControlAppointment(int patientUserId, int? nutritionistUserId, DateTime scheduledAt, string modality)
    {
        PatientUserId = patientUserId;
        NutritionistUserId = nutritionistUserId;
        ScheduledAt = scheduledAt;
        Modality = modality;
        Status = AppointmentStatus.Scheduled.ToString();
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public int? NutritionistUserId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public string Modality { get; private set; }
    public string Status { get; private set; }

    public void MarkReminderSent() => Status = AppointmentStatus.ReminderSent.ToString();
    public void MarkReminderError() => Status = AppointmentStatus.ReminderError.ToString();
    public void Complete() => Status = AppointmentStatus.Completed.ToString();
    public void Cancel() => Status = AppointmentStatus.Cancelled.ToString();
}
