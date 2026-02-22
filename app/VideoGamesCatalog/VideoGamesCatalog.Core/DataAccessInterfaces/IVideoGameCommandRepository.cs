using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.Repositories.Interfaces;

public interface IVideoGameCommandRepository
{
    Task<Guid> AddAsync(VideoGameAddSpecification specification);
    Task UpdateAsync(VideoGameDomain domain);
    Task RemoveAsync(Guid id);
}
