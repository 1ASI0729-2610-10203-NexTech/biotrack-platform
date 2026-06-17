namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public partial class WeightRecord
{
    public WeightRecord() : this(0, DateOnly.MinValue, 0m)
    {
    }

    public WeightRecord(int patientUserId, DateOnly date, decimal weightKg)
    {
        PatientUserId = patientUserId;
        Date = date;
        WeightKg = weightKg;
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal WeightKg { get; private set; }

    public WeightRecord UpdateWeight(decimal weightKg)
    {
        WeightKg = weightKg;
        return this;
    }
}
