using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.Services.Interfaces;
using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DataAccess.Repositories.Interfaces;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.Core.Services;

internal sealed class VideoGameService(
    IVideoGameCommandRepository videoGameCommandRepository,
    IVideoGameQueryRepository videoGameQueryRepository)
    : IVideoGameService
{
    public async Task<Guid> AddAsync(VideoGameAddCommand command)
    {
        return await videoGameCommandRepository.AddAsync(new VideoGameAddSpecification(command.Title, command.Description));
    }

    public async Task<bool> UpdateAsync(VideoGameUpdateCommand command)
    {
        var domainModel = await videoGameQueryRepository.GetByIdAsync(command.Id);

        if (domainModel is null)
        {
            return false;
        }

        domainModel.Update(command.Title, command.Description);

        await videoGameCommandRepository.UpdateAsync(domainModel);
        return true;
    }

    public async Task DeleteAsync(VideoGameDeleteCommand command)
    {
        await videoGameCommandRepository.RemoveAsync(command.Id);
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
