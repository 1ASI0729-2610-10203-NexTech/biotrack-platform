using nextech.biotrack.platform.CorporateManagement.Application.QueryServices;
using nextech.biotrack.platform.CorporateManagement.Domain.Model;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Queries;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.ValueObjects;
using nextech.biotrack.platform.CorporateManagement.Domain.Repositories;

namespace nextech.biotrack.platform.CorporateManagement.Application.Internal.QueryServices;

public class CompanyQueryService(ICompanyRepository companyRepository) : ICompanyQueryService
{
    public async Task<Company?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken) =>
        await companyRepository.FindByIdAsync(query.CompanyId, cancellationToken);

    public async Task<CorporateMetrics?> Handle(
        GetCorporateMetricsByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        var company = await companyRepository.FindByIdWithCollaboratorsAsync(query.CompanyId, cancellationToken);
        if (company == null) return null;

        var collaborators = company.Collaborators;
        var active = CollaboratorStatus.Active.ToString();
        var inactive = CollaboratorStatus.Inactive.ToString();
        var pending = CollaboratorStatus.Pending.ToString();

        return new CorporateMetrics(
            company.Id,
            company.Name,
            collaborators.Count,
            collaborators.Count(c => c.Status == active),
            collaborators.Count(c => c.Status == inactive),
            collaborators.Count(c => c.Status == pending),
            company.UpdatedAt);
    }
}
