using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.DataAccess.ModelConvertors;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.ProjectionModels;
using VideoGamesCatalog.DataAccess.Repositories.Interfaces;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories;

internal sealed class VideoGameQueryRepository(VideoGamesCatalogDbContext dbContext)
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
                Description = videoGame.Description
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
                Description = videoGame.Description
            }).ToListAsync(cancellationToken);

        return entities.ToVideoGameDomains();
    }
}
