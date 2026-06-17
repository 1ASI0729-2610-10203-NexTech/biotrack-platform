namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record RegisterActivityCommand(int PatientUserId, DateOnly Date, string ActivityType, int DurationMinutes, string Intensity);
