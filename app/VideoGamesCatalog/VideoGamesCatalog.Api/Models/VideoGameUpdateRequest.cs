using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record VideoGameUpdateRequest(
    [Required, MaxLength(200)] string Title,
    [MaxLength(2000)] string? Description,
    [Required] byte[] RowVersion);
