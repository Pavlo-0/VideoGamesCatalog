using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record CreateVideoGameRequest(
    [Required, MaxLength(200)] string Title,
    [MaxLength(2000)] string? Description);
