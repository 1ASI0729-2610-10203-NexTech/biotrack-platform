using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Entities;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.ValueObjects;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

public partial class NutritionalPlan
{
    public NutritionalPlan() : this(0, 0, 0, string.Empty, string.Empty, 0)
    {
    }

    public NutritionalPlan(int patientProfileId, int patientUserId, int nutritionistUserId,
        string title, string description, int planDurationDays)
    {
        PatientProfileId = patientProfileId;
        PatientUserId = patientUserId;
        NutritionistUserId = nutritionistUserId;
        Title = title;
        Description = description;
        PlanDurationDays = planDurationDays;
        Status = PlanStatus.Proposed.ToString();
    }

    public int Id { get; }
    public int PatientProfileId { get; private set; }
    public int PatientUserId { get; private set; }
    public int NutritionistUserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int PlanDurationDays { get; private set; }
    public string Status { get; private set; }
    public string? RejectionNotes { get; private set; }
    public ICollection<PlanDay> Days { get; private set; } = new List<PlanDay>();

    public bool IsProposed => Status == PlanStatus.Proposed.ToString();

    public void Activate() => Status = PlanStatus.Activated.ToString();

    public void Reject(string notes)
    {
        Status = PlanStatus.Rejected.ToString();
        RejectionNotes = notes;
    }

    public void AddDay(PlanDay day) => Days.Add(day);
}
