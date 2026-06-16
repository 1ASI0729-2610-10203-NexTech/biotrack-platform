namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model;

public enum NutritionalPlanningError
{
    None,
    PlanNotFound,
    PlanAlreadyExists,
    EvaluationNotFound,
    EvaluationAlreadyExists,
    InvalidMacronutrients,
    InvalidCaloriesTarget,
    InvalidPlanStatus,
    InvalidDayOrMeal,
    InvalidAppointmentDate,
    InvalidAppointmentModality,
    AppointmentNotFound,
    AccessDenied,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
