using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
