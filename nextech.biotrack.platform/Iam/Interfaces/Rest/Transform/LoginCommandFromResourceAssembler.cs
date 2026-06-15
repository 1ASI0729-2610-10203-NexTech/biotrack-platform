using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Iam.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.Iam.Interfaces.Rest.Transform;

public static class LoginCommandFromResourceAssembler
{
    public static LoginCommand ToCommandFromResource(LoginResource resource)
    {
        return new LoginCommand(resource.Email, resource.Password);
    }
}
