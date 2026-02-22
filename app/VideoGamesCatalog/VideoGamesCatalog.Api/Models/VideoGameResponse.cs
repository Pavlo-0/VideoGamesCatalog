namespace VideoGamesCatalog.Api.Models;

public class VideoGameResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<VideoGameGenreResponse> Genres { get; set; } = [];
}