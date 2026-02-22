namespace VideoGamesCatalog.Api.Models;

public class CreateVideoGameRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}
