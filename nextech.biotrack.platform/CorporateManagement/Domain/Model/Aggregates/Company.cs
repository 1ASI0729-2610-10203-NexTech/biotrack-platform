using nextech.biotrack.platform.CorporateManagement.Domain.Model.Entities;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;

public partial class Company
{
    public Company() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0)
    {
    }

    public Company(string name, string ruc, string sector, string country, string city, int ownerId)
    {
        Name = name;
        Ruc = ruc;
        Sector = sector;
        Country = country;
        City = city;
        OwnerId = ownerId;
        Status = CompanyStatus.Active.ToString();
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string Ruc { get; private set; }
    public string Sector { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public int OwnerId { get; private set; }
    public string Status { get; private set; }

    public ICollection<Collaborator> Collaborators { get; private set; } = new List<Collaborator>();

    public void ReplaceCollaborators(
        IEnumerable<(string firstName, string lastName, string email, string documentNumber)> collaboratorData)
    {
        Collaborators.Clear();
        foreach (var (firstName, lastName, email, documentNumber) in collaboratorData)
            Collaborators.Add(new Collaborator(Id, firstName, lastName, email, documentNumber));
    }
}
