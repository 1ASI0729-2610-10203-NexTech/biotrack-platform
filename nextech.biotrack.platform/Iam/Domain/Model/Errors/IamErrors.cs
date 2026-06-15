using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static readonly Error EmailAlreadyTaken =
        new("Iam.EmailAlreadyTaken", "The email address is already registered.");

    public static readonly Error InvalidCredentials =
        new("Iam.InvalidCredentials", "Invalid email or password.");

    public static readonly Error UserNotFound =
        new("Iam.UserNotFound", "The specified user was not found.");

    public static readonly Error InvalidVerificationToken =
        new("Iam.InvalidVerificationToken", "The verification token is invalid or has already been used.");

    public static readonly Error EmailAlreadyVerified =
        new("Iam.EmailAlreadyVerified", "The email address has already been verified.");

    public static readonly Error DatabaseError =
        new("Iam.DatabaseError", "A database error occurred.");

    public static readonly Error InternalServerError =
        new("Iam.InternalServerError", "An unexpected error occurred.");
}
