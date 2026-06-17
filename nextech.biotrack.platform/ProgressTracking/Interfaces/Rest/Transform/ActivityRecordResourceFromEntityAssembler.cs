using nextech.biotrack.platform.ProgressTracking.Domain.Model.Aggregates;
using nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Resources;

namespace nextech.biotrack.platform.ProgressTracking.Interfaces.Rest.Transform;

public static class ActivityRecordResourceFromEntityAssembler
{
    public static ActivityRecordResource ToResourceFromEntity(ActivityRecord entity)
    {
        return new ActivityRecordResource(entity.Id, entity.PatientUserId, entity.Date, entity.ActivityType, entity.DurationMinutes, entity.Intensity);
    }
}
