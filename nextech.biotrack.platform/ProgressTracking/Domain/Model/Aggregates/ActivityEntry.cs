namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public class ActivityEntry
{
    public ActivityEntry() : this(0, string.Empty, 0, 0) { }

    public ActivityEntry(int userId, string activityType, int durationMinutes, int caloriesBurned)
    {
        UserId = userId;
        ActivityType = activityType;
        DurationMinutes = durationMinutes;
        CaloriesBurned = caloriesBurned;
        LoggedAt = DateTime.UtcNow;
    }

    public int Id { get; }
    public int UserId { get; private set; }
    public string ActivityType { get; private set; }
    public int DurationMinutes { get; private set; }
    public int CaloriesBurned { get; private set; }
    public DateTime LoggedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
