namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record ActivityRecordResource(int Id, int PatientUserId, DateOnly Date, string ActivityType, int DurationMinutes, string Intensity);
