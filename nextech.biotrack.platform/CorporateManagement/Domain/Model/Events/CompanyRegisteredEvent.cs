using nextech.biotrack.platform.Shared.Domain.Model.Events;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Events;

public record CompanyRegisteredEvent(int CompanyId, string Name, string Ruc, int OwnerId) : IEvent;
