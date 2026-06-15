using nextech.biotrack.platform.Iam.Application.QueryServices;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Domain.Model.Queries;
using nextech.biotrack.platform.Iam.Domain.Repositories;

namespace nextech.biotrack.platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByEmailAsync(query.Email, cancellationToken);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.ListAsync(cancellationToken);
    }
}
