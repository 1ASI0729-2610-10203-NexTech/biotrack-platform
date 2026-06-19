namespace nextech.biotrack.platform.CorporateManagement.Interfaces.Rest.Resources;

public record CorporateMetricsResource(
    int CompanyId,
    string CompanyName,
    int TotalCollaborators,
    int ActiveCollaborators,
    int InactiveCollaborators,
    int PendingCollaborators,
    DateTimeOffset? LastUpdated);
