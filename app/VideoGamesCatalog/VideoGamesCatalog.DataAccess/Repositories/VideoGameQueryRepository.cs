using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.Core.DataAccessInterfaces;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.DataAccess.Mappings;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.ProjectionModels;

namespace VideoGamesCatalog.DataAccess.Repositories;

internal sealed class VideoGameQueryRepository(IVideoGamesCatalogDbContext dbContext)
: IVideoGameQueryRepository
{
    public async Task<VideoGameDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.VideoGames
            .AsNoTracking()
            .Where(videoGame => videoGame.Id == id)
            .Select(videoGame => new VideoGameProjection
            {
                Id = videoGame.Id,
                Title = videoGame.Title,
                Description = videoGame.Description,
                RowVersion = videoGame.RowVersion
            })
            .FirstOrDefaultAsync(cancellationToken);

        return entity?.ToVideoGameDomain();
    }

    public async Task<IEnumerable<VideoGameDomain>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await dbContext.VideoGames
            .AsNoTracking()
            .Select(videoGame => new VideoGameProjection
            {
                Id = videoGame.Id,
                Title = videoGame.Title,
                Description = videoGame.Description,
                RowVersion = videoGame.RowVersion
            }).ToListAsync(cancellationToken);

        return entities.ToVideoGameDomains();
    }
}
