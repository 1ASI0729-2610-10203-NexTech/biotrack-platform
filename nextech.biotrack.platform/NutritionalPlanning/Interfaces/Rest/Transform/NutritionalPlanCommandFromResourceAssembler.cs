using nextech.biotrack.platform.NutritionalPlanning.Domain.Model.Commands;
using nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.NutritionalPlanning.Interfaces.Rest.Transform;

public static class NutritionalPlanCommandFromResourceAssembler
{
    public static CreateNutritionalPlanCommand ToCommandFromResource(CreateNutritionalPlanResource resource, int nutritionistId) =>
        new(resource.Name, resource.CalorieTarget, resource.ProteinGrams,
            resource.CarbsGrams, resource.FatGrams, nutritionistId);
}
