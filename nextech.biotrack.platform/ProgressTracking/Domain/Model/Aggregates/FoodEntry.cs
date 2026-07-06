namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;

public class FoodEntry
{
    public FoodEntry() : this(0, string.Empty, string.Empty, 0m) { }

    public FoodEntry(int userId, string mealType, string foodName, decimal calories)
    {
        UserId = userId;
        MealType = mealType;
        FoodName = foodName;
        Calories = calories;
        LoggedAt = DateTime.UtcNow;
    }

    public int Id { get; }
    public int UserId { get; private set; }
    public string MealType { get; private set; }
    public string FoodName { get; private set; }
    public decimal Calories { get; private set; }
    public DateTime LoggedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
