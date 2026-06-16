namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;

public record RegisterCompanyCommand(
    string Name,
    string Ruc,
    string Sector,
    string Country,
    string City,
    int OwnerId);
