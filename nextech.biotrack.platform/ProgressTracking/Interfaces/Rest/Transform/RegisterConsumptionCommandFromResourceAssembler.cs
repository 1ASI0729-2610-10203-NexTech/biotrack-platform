using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class RegisterConsumptionCommandFromResourceAssembler
{
    public static RegisterConsumptionCommand ToCommandFromResource(int patientUserId, RegisterConsumptionResource resource)
    {
        return new RegisterConsumptionCommand(patientUserId, resource.Date, resource.MealType, resource.Description, resource.Calories);
    }
}
