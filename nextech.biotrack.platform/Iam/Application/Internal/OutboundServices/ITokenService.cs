using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}
