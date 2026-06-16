namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

public record CompanyResource(
    int Id,
    string Name,
    string Ruc,
    string Sector,
    string Country,
    string City,
    string Status,
    int OwnerId,
    int CollaboratorsCount,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
