using nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.PatientProfile.Application.CommandServices;

public interface IPatientProfileCommandService
{
    Task<Result<PatientProfile>> Handle(RegisterHealthDataCommand command, CancellationToken cancellationToken);
    Task<Result<PatientProfile>> Handle(DefineGoalCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateFoodRestrictionsCommand command, CancellationToken cancellationToken);
}
