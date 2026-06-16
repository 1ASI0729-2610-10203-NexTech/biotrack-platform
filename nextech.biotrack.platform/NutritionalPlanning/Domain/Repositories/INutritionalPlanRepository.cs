using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;

public interface INutritionalPlanRepository : IBaseRepository<NutritionalPlan>
{
    Task<NutritionalPlan?> FindActivePlanByPatientUserIdAsync(int patientUserId, CancellationToken cancellationToken);
    Task<NutritionalPlan?> FindByIdWithDaysAsync(int planId, CancellationToken cancellationToken);
}
