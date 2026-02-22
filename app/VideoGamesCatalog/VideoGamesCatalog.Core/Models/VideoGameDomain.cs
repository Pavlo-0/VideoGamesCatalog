namespace VideoGamesCatalog.Core.Models;

public class VideoGameDomain
{
    public VideoGameDomain(Guid id, string title, string? description, byte[] rowVersion)
    {
        Id = id;
        Title = title;
        Description = description;
        RowVersion = rowVersion;
    }

    public void Update(string title, string? description, byte[] rowVersion)
    {
        Title = title;
        Description = description;
        RowVersion = rowVersion;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
