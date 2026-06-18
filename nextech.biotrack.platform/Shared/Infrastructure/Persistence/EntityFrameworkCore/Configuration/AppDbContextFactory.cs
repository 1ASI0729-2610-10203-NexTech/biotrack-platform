using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "server=localhost;port=3306;database=biotrack_platform;user=root;password=#Mysql123;";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new AppDbContext(optionsBuilder.Options);
    }
}
