using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> FindByVerificationTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.VerificationToken == token, cancellationToken);
    }
}
