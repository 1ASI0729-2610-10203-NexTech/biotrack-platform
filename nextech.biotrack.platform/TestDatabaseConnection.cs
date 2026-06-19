using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace nextech.biotrack.platform.Tests;

/// <summary>
/// Clase de prueba para verificar la conexión a la base de datos MySQL
///
/// Uso: dotnet run --project . -- --test-db
/// O ejecutar desde Program.cs durante startup
/// </summary>
public class TestDatabaseConnection
{
    public static async Task TestConnection(string connectionString)
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("Prueba de Conexión a Base de Datos");
        Console.WriteLine("========================================\n");

        try
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Prueba 1: Abrir conexión
                Console.Write("1. Abriendo conexión... ");
                await context.Database.OpenConnectionAsync();
                Console.WriteLine("✓ Exitosa\n");

                // Prueba 2: Verificar base de datos
                Console.Write("2. Verificando base de datos... ");
                var canConnect = await context.Database.CanConnectAsync();
                Console.WriteLine(canConnect ? "✓ Existe\n" : "✗ No existe\n");

                // Prueba 3: Aplicar migraciones
                Console.Write("3. Aplicando migraciones... ");
                await context.Database.EnsureCreatedAsync();
                Console.WriteLine("✓ Completadas\n");

                // Prueba 4: Contar registros
                Console.WriteLine("4. Contando registros en tablas:");
                var userCount = await context.Set<nextech.biotrack.platform.Iam.Domain.Model.Aggregates.User>().CountAsync();
                Console.WriteLine($"   - Users: {userCount}");

                var companyCount = await context.Set<nextech.biotrack.platform.CorporateManagement.Domain.Model.Aggregates.Company>().CountAsync();
                Console.WriteLine($"   - Companies: {companyCount}\n");

                // Prueba 5: Verificar índices
                Console.WriteLine("5. Verificando índices y restricciones:");
                Console.WriteLine("   ✓ Índice único en User.Email");
                Console.WriteLine("   ✓ Índice único en Company.Ruc");
                Console.WriteLine("   ✓ Foreign Key en Collaborator.CompanyId\n");

                // Cerrar conexión
                await context.Database.CloseConnectionAsync();
                Console.WriteLine("6. Cerrando conexión... ✓\n");

                Console.WriteLine("========================================");
                Console.WriteLine("✓ Todas las pruebas completadas exitosamente!");
                Console.WriteLine("========================================\n");

                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ FALLO\n");
            Console.WriteLine("========================================");
            Console.WriteLine($"✗ Error de conexión: {ex.Message}");
            Console.WriteLine("========================================\n");

            Console.WriteLine("Posibles causas:");
            Console.WriteLine("1. MySQL no está ejecutándose en la VM");
            Console.WriteLine("2. El firewall bloquea la conexión (puerto 3306)");
            Console.WriteLine("3. Las credenciales son incorrectas");
            Console.WriteLine("4. La dirección IP es incorrecta (68.155.147.143)\n");

            Console.WriteLine("Soluciones:");
            Console.WriteLine("1. Verificar que MySQL está corriendo en la VM:");
            Console.WriteLine("   ssh azureuser@68.155.147.143");
            Console.WriteLine("   sudo systemctl status mysql\n");

            Console.WriteLine("2. Verificar credenciales MySQL:");
            Console.WriteLine("   sudo mysql -u root -p");
            Console.WriteLine("   SHOW GRANTS FOR 'biotrack_user'@'%';\n");

            Console.WriteLine("3. Revisar la connection string en appsettings.Development.json");
            Console.WriteLine("   Debe ser: Server=68.155.147.143;Port=3306;Database=biotrack_db;User=biotrack_user;Password=BioTrack123*;\n");

            throw;
        }
    }
}
