using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace nextech.biotrack.platform.CorporateManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CompanyRepository(AppDbContext context) : BaseRepository<Company>(context), ICompanyRepository
{
    public async Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken) =>
        await Context.Set<Company>()
            .AnyAsync(c => c.Ruc == ruc, cancellationToken);
}
