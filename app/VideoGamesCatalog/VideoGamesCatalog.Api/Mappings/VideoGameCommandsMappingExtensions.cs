using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Commands.VideoGame;

namespace VideoGamesCatalog.Api.Extensions;

public static class VideoGameCommandsMappingExtensions
{
    extension(VideoGameAddRequest request)
    {
        public VideoGameAddCommand ToVideoGameAddCommand()
        {
            return new VideoGameAddCommand(request.Title, request.Description);
        }
    }

    extension(VideoGameUpdateRequest request)
    {
        public VideoGameUpdateCommand ToVideoGameUpdateCommand(Guid id)
        {
            return new VideoGameUpdateCommand(id, request.Title, request.Description, request.RowVersion);
        }
    }

    extension(Guid id)
    {
        public VideoGameDeleteCommand ToVideoGameDeleteCommand()
        {
            return new VideoGameDeleteCommand(id);
        }
    }
}

