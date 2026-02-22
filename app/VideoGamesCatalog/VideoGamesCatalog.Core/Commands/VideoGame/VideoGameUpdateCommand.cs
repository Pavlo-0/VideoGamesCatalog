namespace VideoGamesCatalog.Core.Commands.VideoGame
{
    public record VideoGameUpdateCommand(Guid Id, string Title, string? Description);
}
