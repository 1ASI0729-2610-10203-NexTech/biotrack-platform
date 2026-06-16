namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

public record CollaboratorInputResource(
    string FirstName,
    string LastName,
    string Email,
    string DocumentNumber);
