using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories.Interfaces;

public interface IVideoGameCommandRepository
{
    Task AddAsync(VideoGameDomain videoGame);
    void Update(VideoGameDomain videoGame);
    void Remove(Guid id);
}
