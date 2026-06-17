using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class RegisterUserCommandFromResourceAssembler
{
    public static RegisterUserCommand ToCommandFromResource(RegisterUserResource resource)
    {
        return new RegisterUserCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Password,
            resource.Role);
    }
}
