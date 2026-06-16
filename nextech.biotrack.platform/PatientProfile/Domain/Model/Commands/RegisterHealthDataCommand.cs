namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;

public record RegisterHealthDataCommand(
    int PatientUserId,
    decimal CurrentWeightKg,
    decimal HeightCm,
    int Age,
    string BiologicalSex,
    string ActivityLevel,
    int SystolicPressure,
    int DiastolicPressure,
    decimal BasalGlucoseMgDl);
