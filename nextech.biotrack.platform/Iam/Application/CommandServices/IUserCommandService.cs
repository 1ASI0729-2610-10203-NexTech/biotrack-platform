using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken);
}
