using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class ControlAppointmentResourceFromEntityAssembler
{
    public static ControlAppointmentResource ToResourceFromEntity(ControlAppointment appointment) =>
        new(appointment.Id, appointment.PatientUserId, appointment.NutritionistUserId,
            appointment.ScheduledAt, appointment.Modality, appointment.Status,
            appointment.CreatedAt, appointment.UpdatedAt);
}
