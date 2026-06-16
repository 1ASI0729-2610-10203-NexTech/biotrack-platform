using nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;

public static class RegisterCompanyCommandFromResourceAssembler
{
    public static RegisterCompanyCommand ToCommandFromResource(RegisterCompanyResource resource, int ownerId) =>
        new(resource.Name, resource.Ruc, resource.Sector, resource.Country, resource.City, ownerId);
}
