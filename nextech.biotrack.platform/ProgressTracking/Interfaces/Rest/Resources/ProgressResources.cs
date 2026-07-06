namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

public record WeightRecordResource(int Id, decimal WeightKg, string Notes, DateTime RecordedAt);
public record FoodEntryResource(int Id, string MealType, string FoodName, decimal Calories, DateTime LoggedAt);
public record ActivityEntryResource(int Id, string ActivityType, int DurationMinutes, int CaloriesBurned, DateTime LoggedAt);

public record RecordWeightResource(decimal WeightKg, string Notes);
public record LogFoodResource(string MealType, string FoodName, decimal Calories);
public record LogActivityResource(string ActivityType, int DurationMinutes);
