namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record RegisterActivityResource(DateOnly Date, string ActivityType, int DurationMinutes, string Intensity);
