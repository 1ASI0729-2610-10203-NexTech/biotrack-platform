namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public class WeightRecord
{
    public WeightRecord() : this(0, 0m, string.Empty) { }

    public WeightRecord(int userId, decimal weightKg, string notes)
    {
        UserId = userId;
        WeightKg = weightKg;
        Notes = notes;
        RecordedAt = DateTime.UtcNow;
    }

    public int Id { get; }
    public int UserId { get; private set; }
    public decimal WeightKg { get; private set; }
    public string Notes { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
