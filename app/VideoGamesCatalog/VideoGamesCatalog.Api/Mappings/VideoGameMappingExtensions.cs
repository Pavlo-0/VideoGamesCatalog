using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.Api.Extensions;

public static class VideoGameMappingExtensions
{
    extension(VideoGameDomain domain)
    {
        public VideoGameResponse ToVideoGameResponse()
        {
            return new VideoGameResponse
            {
                Id = domain.Id,
                Title = domain.Title,
                Description = domain.Description
            };
        }
    }
}