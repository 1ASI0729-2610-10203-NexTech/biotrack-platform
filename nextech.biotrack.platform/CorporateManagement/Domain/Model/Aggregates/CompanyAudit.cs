using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;

public partial class Company : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
