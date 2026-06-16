using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Entities;

public partial class FoodRestriction : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
