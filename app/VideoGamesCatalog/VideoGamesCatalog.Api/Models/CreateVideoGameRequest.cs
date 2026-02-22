using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record CreateVideoGameRequest(
    [property: Required, MaxLength(200)] string Title,
    [property: MaxLength(2000)] string? Description);
