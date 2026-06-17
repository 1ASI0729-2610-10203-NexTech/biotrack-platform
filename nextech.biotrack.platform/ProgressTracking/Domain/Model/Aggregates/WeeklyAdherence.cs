namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public partial class WeeklyAdherence
{
    public WeeklyAdherence() : this(0, 0, DateOnly.MinValue, 0m)
    {
    }

    public WeeklyAdherence(int patientUserId, int planId, DateOnly weekStart, decimal adherencePct)
    {
        PatientUserId = patientUserId;
        PlanId = planId;
        WeekStart = weekStart;
        AdherencePct = adherencePct;
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public int PlanId { get; private set; }
    public DateOnly WeekStart { get; private set; }
    public decimal AdherencePct { get; private set; }

    public bool IsLowAdherence => AdherencePct < 70m;

    public WeeklyAdherence UpdateAdherence(decimal adherencePct)
    {
        AdherencePct = adherencePct;
        return this;
    }
}
