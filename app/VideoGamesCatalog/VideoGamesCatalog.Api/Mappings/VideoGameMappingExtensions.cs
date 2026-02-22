using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Models;

namespace VideoGamesCatalog.Api.Extensions;

public static class VideoGameMappingExtensions
{
    extension(VideoGameDomain domain)
    {
        public VideoGameResponse ToVideoGameResponse()
        {
            return new VideoGameResponse(domain.Id, domain.Title, domain.Description, domain.RowVersion);
        }
    }
}