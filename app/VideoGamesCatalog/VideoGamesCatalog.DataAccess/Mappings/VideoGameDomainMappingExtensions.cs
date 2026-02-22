using VideoGamesCatalog.DataAccess.Models;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.ModelConvertors;

internal static class VideoGameDomainMappingExtensions
{
    public static VideoGameEntity ToVideoGameEntity(this VideoGameDomain videoGameDomain)
    {
        return new VideoGameEntity
        {
            Id = videoGameDomain.Id,
            Title = videoGameDomain.Title,
            Description = videoGameDomain.Description
        };
    }
}
