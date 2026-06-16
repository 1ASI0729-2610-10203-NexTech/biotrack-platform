namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Services;

public interface INutritionistAssignmentService
{
    Task<bool> IsNutritionistAssignedToPatientAsync(int nutritionistUserId, int patientProfileId,
        CancellationToken cancellationToken);
}
