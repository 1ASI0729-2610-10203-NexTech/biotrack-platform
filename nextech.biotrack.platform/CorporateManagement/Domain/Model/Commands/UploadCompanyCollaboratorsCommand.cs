namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;

public record CollaboratorData(
    string FirstName,
    string LastName,
    string Email,
    string DocumentNumber);

public record UploadCompanyCollaboratorsCommand(
    int CompanyId,
    int RequestingUserId,
    IEnumerable<CollaboratorData> Collaborators);
