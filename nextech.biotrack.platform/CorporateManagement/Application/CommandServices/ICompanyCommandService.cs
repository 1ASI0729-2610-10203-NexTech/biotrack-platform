using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Commands;
using nextech.biotrack.platform.Shared.Application.Internal.Model;

namespace nextech.biotrack.platform.CorporateManagement.Application.CommandServices;

public interface ICompanyCommandService
{
    Task<Result<Company>> Handle(RegisterCompanyCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UploadCompanyCollaboratorsCommand command, CancellationToken cancellationToken);
}
