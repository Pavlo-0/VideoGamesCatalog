using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.DataAccess.EntityModels;

namespace VideoGamesCatalog.DataAccess.Persistence;

/// <summary>
/// Abstraction over the VideoGamesCatalog database context.
/// </summary>
internal interface IVideoGamesCatalogDbContext
{
    DbSet<VideoGameEntity> VideoGames { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
