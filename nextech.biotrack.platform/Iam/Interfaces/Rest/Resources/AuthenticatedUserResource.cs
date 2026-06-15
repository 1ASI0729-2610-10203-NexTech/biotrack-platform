namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Role, string Token);
