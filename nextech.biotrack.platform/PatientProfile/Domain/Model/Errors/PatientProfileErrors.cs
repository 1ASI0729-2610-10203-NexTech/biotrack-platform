using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Errors;

public static class PatientProfileErrors
{
    public static readonly Error ProfileNotFound = new("PatientProfile.ProfileNotFound", "The patient profile was not found.");
    public static readonly Error ProfileAlreadyExists = new("PatientProfile.ProfileAlreadyExists", "A health profile already exists for this patient.");
    public static readonly Error InvalidHealthData = new("PatientProfile.InvalidHealthData", "One or more health data values are invalid.");
    public static readonly Error AccessDenied = new("PatientProfile.AccessDenied", "You do not have access to this patient profile.");
    public static readonly Error InvalidGoal = new("PatientProfile.InvalidGoal", "The specified nutritional goal is invalid.");
    public static readonly Error InvalidRestrictionData = new("PatientProfile.InvalidRestrictionData", "One or more food restrictions have invalid data.");
    public static readonly Error DatabaseError = new("PatientProfile.DatabaseError", "A database error occurred.");
    public static readonly Error InternalServerError = new("PatientProfile.InternalServerError", "An unexpected error occurred.");
}
