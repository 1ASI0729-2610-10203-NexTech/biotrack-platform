namespace nextech.biotrack.platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken);
    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken);
}
