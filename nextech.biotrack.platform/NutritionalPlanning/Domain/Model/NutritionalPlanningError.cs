namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model;

public enum NutritionalPlanningError
{
    PlanNotFound,
    InvalidCalorieTarget,
    InvalidPlanName,
    AccessDenied,
    DatabaseError,
    OperationCancelled,
    InternalServerError
}
