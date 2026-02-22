using Microsoft.AspNetCore.Mvc;
using VideoGamesCatalog.Api.Extensions;
using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Services.Interfaces;

namespace VideoGamesCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGameController(
IVideoGameService videoGameService,
ILogger<VideoGameController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var videoGames = await videoGameService.GetAllAsync(cancellationToken);

        return Ok(videoGames.Select(videoGame => videoGame.ToVideoGameResponse()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VideoGameResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var videoGame = await videoGameService.GetByIdAsync(id, cancellationToken);

        if (videoGame is null)
        {
            logger.LogWarning("Game not found: {Id}", id);
            return NotFound();
        }

        return Ok(videoGame.ToVideoGameResponse());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddAsync(
        [FromBody] VideoGameAddRequest request)
    {
        var id = await videoGameService.AddAsync(request.ToVideoGameAddCommand());

        return id;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        [FromBody] VideoGameUpdateRequest request)
    {
        if (await videoGameService.UpdateAsync(request.ToVideoGameUpdateCommand(id)))
            return NoContent();
        else
            return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await videoGameService.DeleteAsync(id.ToVideoGameDeleteCommand());

        return NoContent();
    }
}
