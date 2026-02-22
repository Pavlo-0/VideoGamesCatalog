using VideoGamesCatalog.DataAccess.ProjectionModels;
using VideoGamesCatalog.DomainModel;

namespace VideoGamesCatalog.DataAccess.ModelConvertors
{
    internal static class VideoGameProjectionMappingExtension
    {
        public static IEnumerable<VideoGameDomain> ToVideoGameDomain(this IEnumerable<VideoGameProjection> videoGameProjections)
        {
            return videoGameProjections.Select(videoGame => videoGame.ToVideoGameDomain());
        }

        public static VideoGameDomain ToVideoGameDomain(this VideoGameProjection videoGameProjection)
        {
            return new VideoGameDomain
            {
                Id = videoGameProjection.Id,
                Title = videoGameProjection.Title,
                Description = videoGameProjection.Description,
                Genres = videoGameProjection.Genres.Select(genre => new VideoGameGenreDomain
                {
                    Id = genre.Id,
                    Name = genre.Name
                }).ToList()
            };
        }
    }
}