namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string FirstName, string LastName, string Email, string Role, bool EmailVerified, string Status, string Token);
