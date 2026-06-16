using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;

public interface IControlAppointmentRepository : IBaseRepository<ControlAppointment>
{
    Task<IEnumerable<ControlAppointment>> FindByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
    Task<IEnumerable<ControlAppointment>> FindScheduledForReminderAsync(CancellationToken cancellationToken);
}
