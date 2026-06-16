namespace nextech.biotrack.platform.PatientProfile.Domain.Model.Entities;

public partial class FoodRestriction
{
    public FoodRestriction() : this(0, string.Empty, string.Empty, string.Empty) { }

    public FoodRestriction(int patientProfileId, string type, string description, string severity)
    {
        PatientProfileId = patientProfileId;
        Type = type;
        Description = description;
        Severity = severity;
    }

    public int Id { get; }
    public int PatientProfileId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public string Severity { get; private set; }

    public Aggregates.PatientProfile PatientProfile { get; private set; } = null!;
}
