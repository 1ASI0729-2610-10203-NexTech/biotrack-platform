using nextech.biotrack.platform.PatientProfile.Application.Internal.Model;
using nextech.biotrack.platform.PatientProfile.Domain.Model.Queries;

namespace nextech.biotrack.platform.PatientProfile.Application.QueryServices;

public interface IPatientProfileQueryService
{
    Task<PatientProfileDto?> Handle(GetPatientProfileQuery query, CancellationToken cancellationToken);
}
