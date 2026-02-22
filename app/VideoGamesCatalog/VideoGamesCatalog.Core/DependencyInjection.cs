using Microsoft.Extensions.DependencyInjection;
using VideoGamesCatalog.Core.Services;
using VideoGamesCatalog.Core.Services.Interfaces;

namespace VideoGamesCatalog.Core;

public static class DependencyInjection
{
    /// <summary>
    /// Registers core services: command and query services for video games.
    /// </summary>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IVideoGameService, VideoGameService>();
        return services;
    }
}
