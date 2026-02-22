using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories.Interfaces;

public interface IVideoGameQueryRepository
{
    Task<VideoGameDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<VideoGameDomain>> GetAllAsync(CancellationToken cancellationToken = default);
}
