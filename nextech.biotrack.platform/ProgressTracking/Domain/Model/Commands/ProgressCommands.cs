namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Commands;

public record RecordWeightCommand(int UserId, decimal WeightKg, string Notes);
public record LogFoodCommand(int UserId, string MealType, string FoodName, decimal Calories);
public record LogActivityCommand(int UserId, string ActivityType, int DurationMinutes);
