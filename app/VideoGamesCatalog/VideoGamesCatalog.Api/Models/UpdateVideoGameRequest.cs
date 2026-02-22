namespace VideoGamesCatalog.Api.Models;

public class UpdateVideoGameRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<VideoGameGenreRequest> Genres { get; set; } = [];
}
