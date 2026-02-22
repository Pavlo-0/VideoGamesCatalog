namespace VideoGamesCatalog.DataAccess.EntityModels;

public class VideoGameEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public byte[] RowVersion { get; set; } = [];
}
