using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Errors;

public static class CorporateManagementErrors
{
    public static readonly Error CompanyNotFound =
        new("CorporateManagement.CompanyNotFound", "The specified company was not found.");

    public static readonly Error RucAlreadyTaken =
        new("CorporateManagement.RucAlreadyTaken", "A company with this RUC is already registered.");

    public static readonly Error InvalidRucFormat =
        new("CorporateManagement.InvalidRucFormat", "The RUC must be exactly 11 digits.");

    public static readonly Error AccessDenied =
        new("CorporateManagement.AccessDenied", "You do not have access to this company.");

    public static readonly Error InvalidCollaboratorData =
        new("CorporateManagement.InvalidCollaboratorData", "One or more collaborators have invalid data.");

    public static readonly Error DatabaseError =
        new("CorporateManagement.DatabaseError", "A database error occurred.");

    public static readonly Error InternalServerError =
        new("CorporateManagement.InternalServerError", "An unexpected error occurred.");
}
