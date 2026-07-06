using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.PatientProfile.Application.CommandServices;

public interface IHealthProfileCommandService
{
    Task<Result<HealthProfile>> Handle(UpdateHealthDataCommand command, CancellationToken cancellationToken);
    Task<Result<HealthProfile>> Handle(UpdateDietaryRestrictionsCommand command, CancellationToken cancellationToken);
}
