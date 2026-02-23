using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VideoGamesCatalog.Core.DataAccessInterfaces;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories;

namespace VideoGamesCatalog.DataAccess;

public static class DependencyInjection
{
    /// <summary>
    /// Registers data access services: DbContext, repositories
    /// </summary>
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<VideoGamesCatalogDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IVideoGamesCatalogDbContext>(sp =>
            sp.GetRequiredService<VideoGamesCatalogDbContext>());

        services.AddScoped<IVideoGameQueryRepository, VideoGameQueryRepository>();
        services.AddScoped<IVideoGameCommandRepository, VideoGameCommandRepository>();

        return services;
    }

    /// <summary>
    /// Applies any pending Entity Framework migrations to the database.
    /// </summary>
    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VideoGamesCatalogDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<VideoGamesCatalogDbContext>>();
        logger.LogInformation("Applying migrations...");
        dbContext.Database.Migrate();
    }
}
