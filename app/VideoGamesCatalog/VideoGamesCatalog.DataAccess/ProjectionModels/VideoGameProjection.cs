namespace VideoGamesCatalog.DataAccess.ProjectionModels
{
    internal class VideoGameProjection
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
