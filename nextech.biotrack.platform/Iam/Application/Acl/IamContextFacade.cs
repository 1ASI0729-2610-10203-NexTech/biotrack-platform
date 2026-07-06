using nextech.biotrack.platform.Iam.Application.QueryServices;
using nextech.biotrack.platform.Iam.Domain.Model.Queries;
using nextech.biotrack.platform.Iam.Interfaces.Acl;

namespace nextech.biotrack.platform.Iam.Application.Acl;

public class IamContextFacade(IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken)
    {
        var query = new GetUserByEmailQuery(email);
        var user = await userQueryService.Handle(query, cancellationToken);
        return user?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(query, cancellationToken);
        return user?.Email ?? string.Empty;
    }
}
