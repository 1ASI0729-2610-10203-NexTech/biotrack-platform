using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates;
using nextech.biotrack.platform.CorporateManagement.Domain.Model.Entities;

namespace nextech.biotrack.platform.CorporateManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyCorporateManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Company>().HasKey(c => c.Id);
        builder.Entity<Company>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Company>().Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Company>().Property(c => c.Ruc).IsRequired().HasMaxLength(11);
        builder.Entity<Company>().HasIndex(c => c.Ruc).IsUnique();
        builder.Entity<Company>().Property(c => c.Sector).IsRequired().HasMaxLength(100);
        builder.Entity<Company>().Property(c => c.Country).IsRequired().HasMaxLength(100);
        builder.Entity<Company>().Property(c => c.City).IsRequired().HasMaxLength(100);
        builder.Entity<Company>().Property(c => c.OwnerId).IsRequired();
        builder.Entity<Company>().Property(c => c.Status).IsRequired().HasMaxLength(20);

        builder.Entity<Collaborator>().HasKey(c => c.Id);
        builder.Entity<Collaborator>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Collaborator>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<Collaborator>().Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<Collaborator>().Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.Entity<Collaborator>().Property(c => c.DocumentNumber).IsRequired().HasMaxLength(20);
        builder.Entity<Collaborator>().Property(c => c.Status).IsRequired().HasMaxLength(20);

        builder.Entity<Company>()
            .HasMany(c => c.Collaborators)
            .WithOne(col => col.Company)
            .HasForeignKey(col => col.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
