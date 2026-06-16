using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;

public partial class PatientProfile : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
