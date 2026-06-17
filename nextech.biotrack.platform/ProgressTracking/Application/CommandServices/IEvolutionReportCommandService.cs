using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.ProgressTracking.Application.CommandServices;

public interface IEvolutionReportCommandService
{
    Task<Result<EvolutionReport>> Handle(GenerateReportCommand command, CancellationToken cancellationToken);
}
