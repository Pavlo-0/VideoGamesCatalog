using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.DataAccess.EntityModels;

namespace VideoGamesCatalog.DataAccess.Mappings;

internal static class VideoGameDomainMappingExtensions
{
    public static VideoGameEntity ToVideoGameEntity(this VideoGameDomain videoGameDomain)
    {
        return new VideoGameEntity
        {
            Id = videoGameDomain.Id,
            Title = videoGameDomain.Title,
            Description = videoGameDomain.Description,
            RowVersion = videoGameDomain.RowVersion
        };
    }
}
