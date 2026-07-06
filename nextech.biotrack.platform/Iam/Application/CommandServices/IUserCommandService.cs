using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken);
    Task<Result<(User user, string token)>> Handle(LoginCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(VerifyEmailCommand command, CancellationToken cancellationToken);
}
