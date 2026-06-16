using nextech.biotrack.platform.Shared.Domain.Model;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Errors;

public static class NutritionalPlanningErrors
{
    public static readonly Error PlanNotFound =
        new("NutritionalPlanning.PlanNotFound", "The nutritional plan was not found.");

    public static readonly Error PlanAlreadyExists =
        new("NutritionalPlanning.PlanAlreadyExists", "A nutritional plan already exists for this patient.");

    public static readonly Error EvaluationNotFound =
        new("NutritionalPlanning.EvaluationNotFound", "The initial evaluation was not found.");

    public static readonly Error EvaluationAlreadyExists =
        new("NutritionalPlanning.EvaluationAlreadyExists", "An initial evaluation already exists for this patient.");

    public static readonly Error InvalidMacronutrients =
        new("NutritionalPlanning.InvalidMacronutrients", "The macronutrient percentages must sum to 100.");

    public static readonly Error InvalidCaloriesTarget =
        new("NutritionalPlanning.InvalidCaloriesTarget", "The calories target must be greater than 0.");

    public static readonly Error InvalidPlanStatus =
        new("NutritionalPlanning.InvalidPlanStatus", "The plan cannot be modified in its current status.");

    public static readonly Error InvalidDayOrMeal =
        new("NutritionalPlanning.InvalidDayOrMeal", "Each day must have at least one valid meal.");

    public static readonly Error InvalidAppointmentDate =
        new("NutritionalPlanning.InvalidAppointmentDate", "The appointment date must be in the future.");

    public static readonly Error InvalidAppointmentModality =
        new("NutritionalPlanning.InvalidAppointmentModality", "The appointment modality is invalid.");

    public static readonly Error AppointmentNotFound =
        new("NutritionalPlanning.AppointmentNotFound", "The control appointment was not found.");

    public static readonly Error AccessDenied =
        new("NutritionalPlanning.AccessDenied", "You do not have access to this resource.");

    public static readonly Error DatabaseError =
        new("NutritionalPlanning.DatabaseError", "A database error occurred.");

    public static readonly Error InternalServerError =
        new("NutritionalPlanning.InternalServerError", "An unexpected error occurred.");
}
