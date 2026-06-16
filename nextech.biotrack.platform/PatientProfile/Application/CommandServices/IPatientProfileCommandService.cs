using nextech.biotrack.platform.PatientProfile.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using HealthProfile = nextech.biotrack.platform.PatientProfile.Domain.Model.Aggregates.PatientProfile;

namespace nextech.biotrack.platform.PatientProfile.Application.CommandServices;

public interface IPatientProfileCommandService
{
    Task<Result<HealthProfile>> Handle(RegisterHealthDataCommand command, CancellationToken cancellationToken);
    Task<Result<HealthProfile>> Handle(DefineGoalCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateFoodRestrictionsCommand command, CancellationToken cancellationToken);
}
