using System.Text.Json.Serialization;

namespace nextech.biotrack.platform.Iam.Domain.Model.Aggregates;

public partial class User
{
    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public User(string firstName, string lastName, string email, string passwordHash, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        EmailVerified = false;
        Status = "PENDING";
        VerificationToken = Guid.NewGuid().ToString();
    }

    public int Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    [JsonIgnore] public string PasswordHash { get; private set; }

    public string Role { get; private set; }
    public bool EmailVerified { get; private set; }
    public string Status { get; private set; }

    [JsonIgnore] public string? VerificationToken { get; private set; }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User VerifyEmail()
    {
        EmailVerified = true;
        Status = "ACTIVE";
        VerificationToken = null;
        return this;
    }
}
