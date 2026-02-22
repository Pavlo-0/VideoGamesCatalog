using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.DataAccess.ModelConvertors;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories.Interfaces;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories;

internal sealed class VideoGameCommandRepository(VideoGamesCatalogDbContext dbContext)
    : IVideoGameCommandRepository
{
    public async Task AddAsync(VideoGameDomain videoGameDomain)
    {
        var entity = videoGameDomain.ToVideoGameEntity();
        await dbContext.VideoGames.AddAsync(entity);
    }

    public void Update(VideoGameDomain videoGameDomain)
    {
        var entity = videoGameDomain.ToVideoGameEntity();
        dbContext.VideoGames.Update(entity);
    }

    public void Remove(VideoGameDomain videoGameDomain)
    {
        var entity = videoGameDomain.ToVideoGameEntity();
        dbContext.VideoGames.Remove(entity);
    }

    public void Remove(Guid id)
    {
        dbContext.VideoGames.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
