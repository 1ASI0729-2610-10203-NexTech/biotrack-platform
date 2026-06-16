using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;

public interface IInitialEvaluationCommandService
{
    Task<Result<InitialEvaluation>> Handle(CompleteEvaluationCommand command, CancellationToken cancellationToken);
}
