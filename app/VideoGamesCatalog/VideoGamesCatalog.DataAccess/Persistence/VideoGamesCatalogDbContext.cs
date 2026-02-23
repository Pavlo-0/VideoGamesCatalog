using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.DataAccess.EntityModels;

namespace VideoGamesCatalog.DataAccess.Persistence;

internal class VideoGamesCatalogDbContext(DbContextOptions<VideoGamesCatalogDbContext> options)
: DbContext(options), IVideoGamesCatalogDbContext
{
    public DbSet<VideoGameEntity> VideoGames => Set<VideoGameEntity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoGamesCatalogDbContext).Assembly);
    }
}
