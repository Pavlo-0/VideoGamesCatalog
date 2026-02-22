using VideoGamesCatalog.Core.Models;

namespace VideoGamesCatalog.Core.DataAccessInterfaces;

public interface IVideoGameQueryRepository
{
    Task<VideoGameDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<VideoGameDomain>> GetAllAsync(CancellationToken cancellationToken = default);
}
