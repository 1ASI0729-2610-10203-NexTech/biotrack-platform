using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.ProgressTracking.Application.CommandServices;

public interface IProgressCommandService
{
    Task<Result<WeightRecord>> Handle(RecordWeightCommand command, CancellationToken cancellationToken);
    Task<Result<FoodEntry>> Handle(LogFoodCommand command, CancellationToken cancellationToken);
    Task<Result<ActivityEntry>> Handle(LogActivityCommand command, CancellationToken cancellationToken);
}
