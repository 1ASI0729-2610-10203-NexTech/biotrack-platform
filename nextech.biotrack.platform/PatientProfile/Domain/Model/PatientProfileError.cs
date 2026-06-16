namespace nextech.biotrack.platform.PatientProfile.Domain.Model;

public enum PatientProfileError
{
    None,
    ProfileNotFound,
    ProfileAlreadyExists,
    InvalidHealthData,
    AccessDenied,
    InvalidGoal,
    InvalidRestrictionData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
