using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class DefineGoalCommandFromResourceAssembler
{
    public static DefineGoalCommand ToCommandFromResource(int patientUserId, DefineGoalResource resource) =>
        new(patientUserId, resource.Goal);
}
