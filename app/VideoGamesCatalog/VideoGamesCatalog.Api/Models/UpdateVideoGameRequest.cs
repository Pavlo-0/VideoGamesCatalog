using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record UpdateVideoGameRequest(
    [property: Required, MaxLength(200)] string Title,
    [property: MaxLength(2000)] string? Description);
