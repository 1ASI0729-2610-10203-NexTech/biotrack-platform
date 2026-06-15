namespace nextech.biotrack.platform.Iam.Domain.Model.Commands;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password, string Role);
