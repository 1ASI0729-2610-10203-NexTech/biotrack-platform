using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;

namespace nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsBillingConfiguration(this ModelBuilder builder)
    {
        builder.Entity<SubscriptionPlan>().HasKey(p => p.Id);
        builder.Entity<SubscriptionPlan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SubscriptionPlan>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<SubscriptionPlan>().Property(p => p.PlanType).IsRequired().HasMaxLength(50);
        builder.Entity<SubscriptionPlan>().Property(p => p.BillingCycle).IsRequired().HasMaxLength(50);
        builder.Entity<SubscriptionPlan>().Property(p => p.MonthlyAmount).IsRequired().HasPrecision(10, 2);
        builder.Entity<SubscriptionPlan>().Property(p => p.IsActive).IsRequired();

        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(s => s.UserId).IsRequired().HasConversion<int>();
        builder.Entity<Subscription>().Property(s => s.PlanId).IsRequired();
        builder.Entity<Subscription>().Property(s => s.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Subscription>().Property(s => s.StartDate).IsRequired();
        builder.Entity<Subscription>().Property(s => s.NextBillingDate).IsRequired();
        builder.Entity<Subscription>().Property(s => s.CancelledDate);

        builder.Entity<CorporateSubscription>().HasKey(c => c.Id);
        builder.Entity<CorporateSubscription>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CorporateSubscription>().Property(c => c.OrganizationUserId).IsRequired().HasConversion<int>();
        builder.Entity<CorporateSubscription>().Property(c => c.PlanId).IsRequired();
        builder.Entity<CorporateSubscription>().Property(c => c.OrganizationName).IsRequired().HasMaxLength(200);
        builder.Entity<CorporateSubscription>().Property(c => c.Status).IsRequired().HasMaxLength(50);
        builder.Entity<CorporateSubscription>().Property(c => c.StartDate).IsRequired();
        builder.Entity<CorporateSubscription>().Property(c => c.NextBillingDate).IsRequired();
        builder.Entity<CorporateSubscription>().Property(c => c.MaxSeats).IsRequired();
        builder.Entity<CorporateSubscription>().Property(c => c.UsedSeats).IsRequired();

        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.SubscriptionId).IsRequired().HasConversion<int>();
        builder.Entity<Payment>().Property(p => p.PaymentDate).IsRequired();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<Payment>().Property(p => p.Status).IsRequired().HasMaxLength(50);
        builder.Entity<Payment>().Property(p => p.TransactionId).IsRequired().HasMaxLength(200);
        builder.Entity<Payment>().Property(p => p.GatewayMessage).IsRequired().HasMaxLength(500);

        builder.Entity<Invoice>().HasKey(i => i.Id);
        builder.Entity<Invoice>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Invoice>().Property(i => i.SubscriptionId).IsRequired().HasConversion<int>();
        builder.Entity<Invoice>().Property(i => i.IssuedDate).IsRequired();
        builder.Entity<Invoice>().Property(i => i.DueDate).IsRequired();
        builder.Entity<Invoice>().Property(i => i.Amount).IsRequired().HasPrecision(10, 2);
        builder.Entity<Invoice>().Property(i => i.IsPaid).IsRequired();

        builder.Entity<LatePaymentNotice>().HasKey(n => n.Id);
        builder.Entity<LatePaymentNotice>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<LatePaymentNotice>().Property(n => n.SubscriptionId).IsRequired().HasConversion<int>();
        builder.Entity<LatePaymentNotice>().Property(n => n.NoticeDate).IsRequired();
        builder.Entity<LatePaymentNotice>().Property(n => n.FailedAttempts).IsRequired();
        builder.Entity<LatePaymentNotice>().Property(n => n.Status).IsRequired().HasMaxLength(50);
    }
}
