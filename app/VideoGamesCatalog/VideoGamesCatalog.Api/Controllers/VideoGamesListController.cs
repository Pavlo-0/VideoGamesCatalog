using Microsoft.AspNetCore.Mvc;
using VideoGamesCatalog.Api.Extensions;
using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Services.Interfaces;

namespace VideoGamesCatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGamesListController(IVideoGameService videoGameService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var videoGames = await videoGameService.GetAllAsync(cancellationToken);

        return Ok(videoGames.Select(videoGame => videoGame.ToVideoGameResponse()));
    }
}
