namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;

public record DefineGoalCommand(int PatientUserId, string Goal);
