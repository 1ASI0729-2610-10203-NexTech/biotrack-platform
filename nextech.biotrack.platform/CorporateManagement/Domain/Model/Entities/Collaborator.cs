using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Entities;

public partial class Collaborator
{
    public Collaborator() : this(0, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public Collaborator(int companyId, string firstName, string lastName, string email, string documentNumber)
    {
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DocumentNumber = documentNumber;
        Status = CollaboratorStatus.Pending.ToString();
    }

    public int Id { get; }
    public int CompanyId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string DocumentNumber { get; private set; }
    public string Status { get; private set; }

    public Company Company { get; private set; } = null!;
}
