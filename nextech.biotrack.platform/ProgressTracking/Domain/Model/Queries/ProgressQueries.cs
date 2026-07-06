namespace nextech.biotrack.platform.ProgressTracking.Domain.Model.Queries;

public record GetWeightProgressByUserIdQuery(int UserId);
public record GetActivityHistoryByUserIdQuery(int UserId);
public record GetFoodLogsByUserIdQuery(int UserId);
