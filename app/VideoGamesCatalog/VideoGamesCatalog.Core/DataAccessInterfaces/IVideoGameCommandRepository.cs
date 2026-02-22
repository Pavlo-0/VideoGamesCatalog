using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Specification;

namespace VideoGamesCatalog.Core.DataAccessInterfaces;

public interface IVideoGameCommandRepository
{
    Task<Guid> AddAsync(VideoGameAddSpecification specification);
    Task UpdateAsync(VideoGameDomain domain);
    Task RemoveAsync(Guid id);
}
