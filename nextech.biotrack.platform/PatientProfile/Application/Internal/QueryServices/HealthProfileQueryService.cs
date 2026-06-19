using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;

namespace nextech.biotrack.platform.PatientProfile.Application.Internal.QueryServices;

public class HealthProfileQueryService(IHealthProfileRepository profileRepository) : IHealthProfileQueryService
{
    public async Task<HealthProfile?> Handle(GetHealthProfileByUserIdQuery query, CancellationToken cancellationToken) =>
        await profileRepository.FindByUserIdAsync(query.UserId, cancellationToken);
}
