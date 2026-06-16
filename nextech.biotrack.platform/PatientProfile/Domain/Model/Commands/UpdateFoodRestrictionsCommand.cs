namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;

public record RestrictionData(string Type, string Description, string Severity);

public record UpdateFoodRestrictionsCommand(
    int PatientUserId,
    IEnumerable<RestrictionData> Restrictions);
