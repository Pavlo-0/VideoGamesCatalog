using VideoGamesCatalog.DataAccess.Models;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.ModelConvertors;

internal static class VideoGameEntityMappingExtensions
{
    public static VideoGameEntity ToVideoGameEntity(this VideoGameDomain videoGameDomain)
    {
        return new VideoGameEntity
        {
            Id = videoGameDomain.Id,
            Title = videoGameDomain.Title,
            Description = videoGameDomain.Description,
            Genres = videoGameDomain.Genres.Select(genre => new GenreEntity
            {
                Id = genre.Id.GetValueOrDefault(),
                Name = genre.Name
            }).ToList()
        };
    }
}
