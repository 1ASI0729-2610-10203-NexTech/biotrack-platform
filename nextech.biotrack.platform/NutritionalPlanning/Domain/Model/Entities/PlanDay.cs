using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Entities;

public class PlanDay
{
    public PlanDay() : this(0, string.Empty)
    {
    }

    public PlanDay(int nutritionalPlanId, string dayOfWeek)
    {
        NutritionalPlanId = nutritionalPlanId;
        DayOfWeek = dayOfWeek;
    }

    public int Id { get; }
    public int NutritionalPlanId { get; private set; }
    public string DayOfWeek { get; private set; }
    public ICollection<Meal> Meals { get; private set; } = new List<Meal>();
    public NutritionalPlan NutritionalPlan { get; private set; } = null!;

    public void AddMeal(Meal meal) => Meals.Add(meal);
}
