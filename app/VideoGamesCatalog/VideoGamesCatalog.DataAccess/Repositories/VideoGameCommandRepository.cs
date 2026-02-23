using Microsoft.EntityFrameworkCore;
using VideoGamesCatalog.Core.DataAccessInterfaces;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DataAccess.Mappings;
using VideoGamesCatalog.DataAccess.Persistence;

namespace VideoGamesCatalog.DataAccess.Repositories;

internal sealed class VideoGameCommandRepository(IVideoGamesCatalogDbContext dbContext)
: IVideoGameCommandRepository
{
    public async Task<Guid> AddAsync(VideoGameAddSpecification specification)
    {
        var entity = specification.ToVideoGameEntity();
        dbContext.VideoGames.Add(entity);
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
