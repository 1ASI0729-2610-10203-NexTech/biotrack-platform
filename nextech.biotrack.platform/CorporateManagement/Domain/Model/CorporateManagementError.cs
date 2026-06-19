namespace nextech.biotrack.platform.CorporateManagement.Domain.Model;

public enum CorporateManagementError
{
    None,
    CompanyNotFound,
    RucAlreadyTaken,
    InvalidRucFormat,
    AccessDenied,
    InvalidCollaboratorData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
