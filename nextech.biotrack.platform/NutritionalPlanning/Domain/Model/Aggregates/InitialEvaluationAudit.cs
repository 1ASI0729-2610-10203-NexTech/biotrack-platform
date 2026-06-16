using nextech.biotrack.platform.Shared.Domain.Model.Entities;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

public partial class InitialEvaluation : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
