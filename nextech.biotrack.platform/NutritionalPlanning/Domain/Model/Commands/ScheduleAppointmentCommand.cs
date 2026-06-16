namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;

public record ScheduleAppointmentCommand(int PatientUserId, int? NutritionistUserId, DateTime ScheduledAt, string Modality);
