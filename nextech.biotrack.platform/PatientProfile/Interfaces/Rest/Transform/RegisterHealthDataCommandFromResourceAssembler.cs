using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Transform;

public static class RegisterHealthDataCommandFromResourceAssembler
{
    public static RegisterHealthDataCommand ToCommandFromResource(
        int patientUserId, RegisterHealthDataResource resource) =>
        new(patientUserId,
            resource.CurrentWeightKg,
            resource.HeightCm,
            resource.Age,
            resource.BiologicalSex,
            resource.ActivityLevel,
            resource.SystolicPressure,
            resource.DiastolicPressure,
            resource.BasalGlucoseMgDl);
}
