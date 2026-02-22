using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.DataAccess.Models;

namespace VideoGamesCatalog.DataAccess.Persistence;

public class VideoGamesCatalogDbContext(DbContextOptions<VideoGamesCatalogDbContext> options)
    : DbContext(options)
{
    public DbSet<VideoGameEntity> VideoGames => Set<VideoGameEntity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideoGamesCatalogDbContext).Assembly);
    }
}
