namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

public record RegisterCompanyResource(
    string Name,
    string Ruc,
    string Sector,
    string Country,
    string City);
