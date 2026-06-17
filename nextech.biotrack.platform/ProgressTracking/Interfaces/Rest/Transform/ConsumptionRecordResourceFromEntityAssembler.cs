using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class ConsumptionRecordResourceFromEntityAssembler
{
    public static ConsumptionRecordResource ToResourceFromEntity(ConsumptionRecord entity)
    {
        return new ConsumptionRecordResource(entity.Id, entity.PatientUserId, entity.Date, entity.MealType, entity.Description, entity.Calories);
    }
}
