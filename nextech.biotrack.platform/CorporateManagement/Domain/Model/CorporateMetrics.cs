namespace nextech.biotrack.platform.CorporateManagement.Domain.Model;

public record CorporateMetrics(
    int CompanyId,
    string CompanyName,
    int TotalCollaborators,
    int ActiveCollaborators,
    int InactiveCollaborators,
    int PendingCollaborators,
    DateTimeOffset? LastUpdated);
