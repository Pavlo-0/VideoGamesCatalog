using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DataAccess.ModelConvertors;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories.Interfaces;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories;

internal sealed class VideoGameCommandRepository(VideoGamesCatalogDbContext dbContext)
    : IVideoGameCommandRepository
{
    public async Task<Guid> AddAsync(VideoGameAddSpecification specification)
    {
        var entity = specification.ToVideoGameEntity();
        await dbContext.VideoGames.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(VideoGameDomain videoGameDomain)
    {
        var entity = videoGameDomain.ToVideoGameEntity();
        dbContext.VideoGames.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
         await dbContext.VideoGames.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
}
