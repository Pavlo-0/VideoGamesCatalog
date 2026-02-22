using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.Models;

namespace VideoGamesCatalog.Core.Services.Interfaces;

public interface IVideoGameService
{
    Task<Guid> AddAsync(VideoGameAddCommand command);
    Task<bool> UpdateAsync(VideoGameUpdateCommand command);
    Task DeleteAsync(VideoGameDeleteCommand command);

    Task<VideoGameDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<VideoGameDomain>> GetAllAsync(CancellationToken cancellationToken = default);
}
