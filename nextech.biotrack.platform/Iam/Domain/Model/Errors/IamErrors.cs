namespace nextech.biotrack.platform.Iam.Domain.Model.Errors;

public enum IamErrors
{
    None,
    UserNotFound,
    EmailAlreadyTaken,
    InvalidCredentials,
    InvalidVerificationToken,
    EmailAlreadyVerified,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
