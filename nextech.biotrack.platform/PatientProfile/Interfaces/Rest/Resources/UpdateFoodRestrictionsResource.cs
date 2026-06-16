namespace nextech.biotrack.platform.PatientProfile.Interfaces.Rest.Resources;

public record UpdateFoodRestrictionsResource(IEnumerable<FoodRestrictionItemResource> Restrictions);
