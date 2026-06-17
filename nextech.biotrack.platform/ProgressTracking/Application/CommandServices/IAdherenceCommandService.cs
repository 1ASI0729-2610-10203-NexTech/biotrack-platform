using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.ProgressTracking.Application.CommandServices;

public interface IAdherenceCommandService
{
    Task<Result<WeeklyAdherence>> Handle(CalculateAdherenceCommand command, CancellationToken cancellationToken);
}
