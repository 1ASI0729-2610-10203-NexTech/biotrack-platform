using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySQL("Server=localhost;Database=biotrack_dev;User=root;Password=root;");
        return new AppDbContext(optionsBuilder.Options);
    }
}
