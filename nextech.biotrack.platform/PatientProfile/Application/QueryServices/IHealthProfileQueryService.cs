using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;

namespace nextech.biotrack.platform.PatientProfile.Application.QueryServices;

public interface IHealthProfileQueryService
{
    Task<HealthProfile?> Handle(GetHealthProfileByUserIdQuery query, CancellationToken cancellationToken);
}
