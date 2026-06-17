using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class RegisterActivityCommandFromResourceAssembler
{
    public static RegisterActivityCommand ToCommandFromResource(int patientUserId, RegisterActivityResource resource)
    {
        return new RegisterActivityCommand(patientUserId, resource.Date, resource.ActivityType, resource.DurationMinutes, resource.Intensity);
    }
}
