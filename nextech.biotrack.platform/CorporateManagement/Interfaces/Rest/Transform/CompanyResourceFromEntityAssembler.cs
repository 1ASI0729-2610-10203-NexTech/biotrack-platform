using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;

public static class CompanyResourceFromEntityAssembler
{
    public static CompanyResource ToResourceFromEntity(Company company) =>
        new(company.Id,
            company.Name,
            company.Ruc,
            company.Sector,
            company.Country,
            company.City,
            company.Status,
            company.OwnerId,
            company.Collaborators.Count,
            company.CreatedAt,
            company.UpdatedAt);
}
