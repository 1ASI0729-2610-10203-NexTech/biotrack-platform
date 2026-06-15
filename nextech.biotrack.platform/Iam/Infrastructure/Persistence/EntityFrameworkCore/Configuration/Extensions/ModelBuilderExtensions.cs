using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;

namespace nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.EmailVerified).IsRequired();
        builder.Entity<User>().Property(u => u.Status).IsRequired().HasMaxLength(20);
        builder.Entity<User>().Property(u => u.VerificationToken).HasMaxLength(100);
    }
}
