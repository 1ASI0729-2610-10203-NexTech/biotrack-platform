using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Events;

public record CompanyCollaboratorsUploadedEvent(int CompanyId, int CollaboratorsCount) : IEvent;
