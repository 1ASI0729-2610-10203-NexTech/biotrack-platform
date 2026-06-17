using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class WeightRecordResourceFromEntityAssembler
{
    public static WeightRecordResource ToResourceFromEntity(WeightRecord entity)
    {
        return new WeightRecordResource(entity.Id, entity.PatientUserId, entity.Date, entity.WeightKg);
    }
}
