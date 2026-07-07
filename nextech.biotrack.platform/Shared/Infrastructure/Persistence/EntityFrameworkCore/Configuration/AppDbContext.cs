using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.CorporateManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyIamConfiguration();
        builder.ApplyCorporateManagementConfiguration();
        builder.ApplyNutritionalPlanningConfiguration();
        builder.ApplyPatientProfileConfiguration();
        builder.ApplyProgressTrackingConfiguration();
        builder.ApplySubscriptionsBillingConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
