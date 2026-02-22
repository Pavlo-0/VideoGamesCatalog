using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DataAccess.EntityModels;

namespace VideoGamesCatalog.DataAccess.Mappings;

internal static class VideoGameSpecificationMappingExtensions
{
    public static VideoGameEntity ToVideoGameEntity(this VideoGameAddSpecification specification)
    {
        return new VideoGameEntity()
        {
            Title = specification.Title,
            Description = specification.Description
        };
    }
}