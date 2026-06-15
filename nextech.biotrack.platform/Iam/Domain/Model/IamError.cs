namespace nextech.biotrack.platform.Iam.Domain.Model;

public enum IamError
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
