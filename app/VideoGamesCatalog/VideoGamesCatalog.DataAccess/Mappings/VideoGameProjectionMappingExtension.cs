using VideoGamesCatalog.DataAccess.ProjectionModels;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.ModelConvertors
{
    internal static class VideoGameProjectionMappingExtension
    {
        public static IEnumerable<VideoGameDomain> ToVideoGameDomains(this IEnumerable<VideoGameProjection> videoGameProjections)
        {
            return videoGameProjections.Select(videoGame => videoGame.ToVideoGameDomain());
        }

        public static VideoGameDomain ToVideoGameDomain(this VideoGameProjection videoGameProjection)
        {
            return new VideoGameDomain(
                videoGameProjection.Id,
                videoGameProjection.Title,
                videoGameProjection.Description);
        }
    }
}