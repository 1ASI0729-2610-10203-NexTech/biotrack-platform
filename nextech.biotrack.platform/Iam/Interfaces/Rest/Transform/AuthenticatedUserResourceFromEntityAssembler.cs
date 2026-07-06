using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.EmailVerified, user.Status, token);
    }
}
