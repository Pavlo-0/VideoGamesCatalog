namespace VideoGamesCatalog.DataAccess.Models;

public class GenreEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<VideoGameEntity> VideoGames { get; set; } = [];
}
