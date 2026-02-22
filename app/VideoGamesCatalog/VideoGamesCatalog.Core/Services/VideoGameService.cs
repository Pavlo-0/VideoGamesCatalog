using Microsoft.Extensions.Logging;
using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.DataAccessInterfaces;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Services.Interfaces;
using VideoGamesCatalog.Core.Specification;

namespace VideoGamesCatalog.Core.Services;

internal sealed class VideoGameService(
    IVideoGameCommandRepository videoGameCommandRepository,
    IVideoGameQueryRepository videoGameQueryRepository,
    ILogger<VideoGameService> logger)
    : IVideoGameService
{
    public async Task<Guid> AddAsync(VideoGameAddCommand command)
    {
        var id = await videoGameCommandRepository.AddAsync(new VideoGameAddSpecification(command.Title, command.Description));
        logger.LogInformation("Game added: {Id}", id);
        return id;
    }

    public async Task<bool> UpdateAsync(VideoGameUpdateCommand command)
    {
        var domainModel = await videoGameQueryRepository.GetByIdAsync(command.Id);

        if (domainModel is null)
        {
            logger.LogWarning("Game not found for update: {Id}", command.Id);
            return false;
        }

        domainModel.Update(command.Title, command.Description, command.RowVersion);

        await videoGameCommandRepository.UpdateAsync(domainModel);
        logger.LogInformation("Game updated: {Id}", command.Id);
        return true;
    }

    public async Task DeleteAsync(VideoGameDeleteCommand command)
    {
        await videoGameCommandRepository.RemoveAsync(command.Id);
        logger.LogInformation("Game deleted: {Id}", command.Id);
    }

    public Task<VideoGameDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return videoGameQueryRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<IEnumerable<VideoGameDomain>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return videoGameQueryRepository.GetAllAsync(cancellationToken);
    }
}
