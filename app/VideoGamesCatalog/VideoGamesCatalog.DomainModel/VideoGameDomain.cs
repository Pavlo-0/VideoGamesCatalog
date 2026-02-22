namespace VideoGamesCatalog.DomainModel
{
    public class VideoGameDomain
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<VideoGameGenreDomain> Genres { get; set; } = [];
    }
}
