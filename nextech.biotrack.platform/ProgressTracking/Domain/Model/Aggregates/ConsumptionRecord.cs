namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public partial class ConsumptionRecord
{
    public ConsumptionRecord() : this(0, DateOnly.MinValue, string.Empty, string.Empty, 0)
    {
    }

    public ConsumptionRecord(int patientUserId, DateOnly date, string mealType, string description, int calories)
    {
        PatientUserId = patientUserId;
        Date = date;
        MealType = mealType;
        Description = description;
        Calories = calories;
    }

    public int Id { get; }
    public int PatientUserId { get; private set; }
    public DateOnly Date { get; private set; }
    public string MealType { get; private set; }
    public string Description { get; private set; }
    public int Calories { get; private set; }
}
