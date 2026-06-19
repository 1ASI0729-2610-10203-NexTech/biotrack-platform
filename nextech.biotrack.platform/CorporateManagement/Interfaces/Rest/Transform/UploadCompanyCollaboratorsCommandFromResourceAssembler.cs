using nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;
using nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Transform;

public static class UploadCompanyCollaboratorsCommandFromResourceAssembler
{
    public static UploadCompanyCollaboratorsCommand ToCommandFromResource(
        int companyId, int requestingUserId, UploadCollaboratorsResource resource) =>
        new(companyId,
            requestingUserId,
            resource.Collaborators.Select(c =>
                new CollaboratorData(c.FirstName, c.LastName, c.Email, c.DocumentNumber)));
}
