namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

public record RegisterHealthDataResource(
    decimal CurrentWeightKg,
    decimal HeightCm,
    int Age,
    string BiologicalSex,
    string ActivityLevel,
    int SystolicPressure,
    int DiastolicPressure,
    decimal BasalGlucoseMgDl);
