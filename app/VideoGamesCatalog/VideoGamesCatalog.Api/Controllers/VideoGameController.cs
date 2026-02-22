using Microsoft.AspNetCore.Mvc;
using VideoGamesCatalog.Api.Extensions;
using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Services.Interfaces;

namespace VideoGamesCatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGameDetailController(
    IVideoGameService videoGameService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VideoGameResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var videoGame = await videoGameService.GetByIdAsync(id, cancellationToken);

        if (videoGame is null)
        {
            return NotFound();
        }

        return Ok(videoGame.ToVideoGameResponse());
    }

    [HttpPost]
    public async Task<ActionResult<VideoGameResponse>> CreateAsync(
        [FromBody] CreateVideoGameRequest request)
    {
        var id = await videoGameService.AddAsync(request.ToVideoGameAddCommand());

        return CreatedAtAction(nameof(GetByIdAsync), new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        [FromBody] UpdateVideoGameRequest request)
    {
        try
        {
            await videoGameService.UpdateAsync(request.ToVideoGameUpdateCommand(id));
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await videoGameService.DeleteAsync(id.ToVideoGameDeleteCommand());

        return NoContent();
    }
}
