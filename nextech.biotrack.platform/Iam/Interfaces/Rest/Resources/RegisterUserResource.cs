namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

public record RegisterUserResource(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role);
