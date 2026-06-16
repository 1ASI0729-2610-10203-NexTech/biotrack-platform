using nextech.biotrack.platform.NutritionalPlanning.Domain.Services;

namespace nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Services;

public class NutritionistAssignmentService : INutritionistAssignmentService
{
    public Task<bool> IsNutritionistAssignedToPatientAsync(
        int nutritionistUserId, int patientProfileId, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}
