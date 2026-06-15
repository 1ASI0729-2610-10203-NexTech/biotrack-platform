using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.EmailVerified,
            user.Status);
    }
}
