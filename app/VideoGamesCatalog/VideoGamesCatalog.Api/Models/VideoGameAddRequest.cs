using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record VideoGameAddRequest(
    [Required, MaxLength(200)] string Title,
    [MaxLength(2000)] string? Description);
