using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories;
using VideoGamesCatalog.DataAccess.Repositories.Interfaces;

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

        services.AddScoped<IVideoGameQueryRepository, VideoGameQueryRepository>();
        services.AddScoped<IVideoGameCommandRepository, VideoGameCommandRepository>();

        return services;
    }
}
