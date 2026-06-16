using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ControlAppointmentRepository(AppDbContext context)
    : BaseRepository<ControlAppointment>(context), IControlAppointmentRepository
{
    public async Task<IEnumerable<ControlAppointment>> FindByPatientUserIdAsync(
        int patientUserId, CancellationToken cancellationToken)
    {
        return await Context.Set<ControlAppointment>()
            .Where(a => a.PatientUserId == patientUserId)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ControlAppointment>> FindScheduledForReminderAsync(
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var threshold = now.AddHours(24);
        return await Context.Set<ControlAppointment>()
            .Where(a => a.Status == AppointmentStatus.Scheduled.ToString()
                        && a.ScheduledAt > now
                        && a.ScheduledAt <= threshold)
            .ToListAsync(cancellationToken);
    }
}
