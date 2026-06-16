using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class ScheduleAppointmentCommandFromResourceAssembler
{
    public static ScheduleAppointmentCommand ToCommandFromResource(int patientUserId, ScheduleAppointmentResource resource) =>
        new(patientUserId, null, resource.ScheduledAt, resource.Modality);
}
