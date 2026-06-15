using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

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
        builder.UseSnakeCaseNamingConvention();
        builder.ApplyIamConfiguration();
    }
}
