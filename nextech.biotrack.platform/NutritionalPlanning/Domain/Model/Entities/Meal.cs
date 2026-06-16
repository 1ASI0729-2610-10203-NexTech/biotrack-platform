namespace nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Entities;

public class Meal
{
    public Meal() : this(0, string.Empty, string.Empty, string.Empty, 0)
    {
    }

    public Meal(int planDayId, string type, string name, string description, int calories)
    {
        PlanDayId = planDayId;
        Type = type;
        Name = name;
        Description = description;
        Calories = calories;
    }

    public int Id { get; }
    public int PlanDayId { get; private set; }
    public string Type { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Calories { get; private set; }
    public PlanDay PlanDay { get; private set; } = null!;
}
