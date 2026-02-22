using System.ComponentModel.DataAnnotations;

namespace VideoGamesCatalog.Api.Models;

public record VideoGameResponse([Required] Guid Id, [Required] string Title, string? Description, [Required] byte[] RowVersion);
