using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<CorporateSubscription> CorporateSubscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<LatePaymentNotice> LatePaymentNotices { get; set; }

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
        builder.ApplySubscriptionsBillingConfiguration();
    }
}
