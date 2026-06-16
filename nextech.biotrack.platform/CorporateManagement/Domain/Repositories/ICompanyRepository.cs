using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken);
    Task<Company?> FindByIdWithCollaboratorsAsync(int companyId, CancellationToken cancellationToken);
}
