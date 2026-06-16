namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public partial class ActivityRecord
{
    public ActivityRecord() : this(0, DateOnly.MinValue, string.Empty, 0, string.Empty)
    {
    }

    public ActivityRecord(int patientUserId, DateOnly date, string activityType, int durationMinutes, string intensity)
    {
        PatientUserId = patientUserId;
        Date = date;
        ActivityType = activityType;
        DurationMinutes = durationMinutes;
        Intensity = intensity;
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public DateOnly Date { get; private set; }
    public string ActivityType { get; private set; }
    public int DurationMinutes { get; private set; }
    public string Intensity { get; private set; }
}
