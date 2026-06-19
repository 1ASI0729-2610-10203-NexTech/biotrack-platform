namespace nextech.biotrack.platform.PatientProfile.Domain.Model;

public enum PatientProfileError
{
    ProfileNotFound,
    ProfileAlreadyExists,
    InvalidHealthData,
    AccessDenied,
    DatabaseError,
    OperationCancelled,
    InternalServerError
}
