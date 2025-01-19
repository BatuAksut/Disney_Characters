
using Disney_Characters;
using Microsoft.EntityFrameworkCore;
using Serilog;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DisneyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDisneyCharacterService, DisneyCharacterService>();

        return services;
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DisneyDbContext>();
                await context.Database.MigrateAsync();

                // İsteğe bağlı: İlk verileri API'den çek
                var disneyService = services.GetRequiredService<IDisneyCharacterService>();
                await disneyService.SeedDataFromApiAsync();

                Log.Information("Database initialized successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while initializing the database");
                throw;
            }
        }
    }
}