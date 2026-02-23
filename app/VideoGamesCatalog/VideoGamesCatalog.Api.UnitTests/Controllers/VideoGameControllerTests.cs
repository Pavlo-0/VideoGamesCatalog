using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using VideoGamesCatalog.Api.Controllers;
using VideoGamesCatalog.Api.Models;
using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Services.Interfaces;

namespace VideoGamesCatalog.Api.UnitTests.Controllers;

public sealed class VideoGameControllerTests
{
    private readonly IVideoGameService _videoGameService = Substitute.For<IVideoGameService>();
    private readonly VideoGameController _controller;

    public VideoGameControllerTests()
    {
        _controller = new VideoGameController(
            _videoGameService,
            NullLogger<VideoGameController>.Instance);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOkWithMappedVideoGames()
    {
        var games = new[]
        {
            new VideoGameDomain(Guid.NewGuid(), "Halo", "Shooter", [1, 2]),
            new VideoGameDomain(Guid.NewGuid(), "Zelda", null, [3, 4])
        };
        _videoGameService
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(games);

        var result = await _controller.GetAllAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var responses = Assert.IsAssignableFrom<IEnumerable<VideoGameResponse>>(okResult.Value);
        var list = responses.ToList();
        Assert.Equal(2, list.Count);
        Assert.Equal(games[0].Id, list[0].Id);
        Assert.Equal(games[0].Title, list[0].Title);
        Assert.Equal(games[1].Id, list[1].Id);
        Assert.Equal(games[1].Title, list[1].Title);
    }

    [Fact]
    public async Task GetAllAsync_WhenNoGamesExist_ReturnsOkWithEmptyCollection()
    {
        _videoGameService
            .GetAllAsync(Arg.Any<CancellationToken>())
            .Returns(Enumerable.Empty<VideoGameDomain>());

        var result = await _controller.GetAllAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var responses = Assert.IsAssignableFrom<IEnumerable<VideoGameResponse>>(okResult.Value);
        Assert.Empty(responses);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameExists_ReturnsOkWithMappedVideoGame()
    {
        var id = Guid.NewGuid();
        var game = new VideoGameDomain(id, "Halo", "Shooter", [1, 2]);
        _videoGameService
            .GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns(game);

        var result = await _controller.GetByIdAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<VideoGameResponse>(okResult.Value);
        Assert.Equal(id, response.Id);
        Assert.Equal("Halo", response.Title);
        Assert.Equal("Shooter", response.Description);
        Assert.Equal(game.RowVersion, response.RowVersion);
    }

    [Fact]
    public async Task GetByIdAsync_WhenGameDoesNotExist_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        _videoGameService
            .GetByIdAsync(id, Arg.Any<CancellationToken>())
            .Returns((VideoGameDomain?)null);

        var result = await _controller.GetByIdAsync(id, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddAsync_ReturnsNewIdentifier()
    {
        var expectedId = Guid.NewGuid();
        var request = new VideoGameAddRequest("Halo", "Shooter");
        _videoGameService
            .AddAsync(Arg.Is<VideoGameAddCommand>(c =>
                c.Title == request.Title && c.Description == request.Description))
            .Returns(expectedId);

        var result = await _controller.AddAsync(request);

        Assert.Equal(expectedId, result.Value);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameIsUpdated_ReturnsNoContent()
    {
        var id = Guid.NewGuid();
        var request = new VideoGameUpdateRequest("Updated Title", "Updated Desc", [1, 2]);
        _videoGameService
            .UpdateAsync(Arg.Is<VideoGameUpdateCommand>(c =>
                c.Id == id && c.Title == request.Title &&
                c.Description == request.Description && c.RowVersion == request.RowVersion))
            .Returns(true);

        var result = await _controller.UpdateAsync(id, request);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenGameIsNotFound_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        var request = new VideoGameUpdateRequest("Title", "Desc", [1, 2]);
        _videoGameService
            .UpdateAsync(Arg.Any<VideoGameUpdateCommand>())
            .Returns(false);

        var result = await _controller.UpdateAsync(id, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNoContent()
    {
        var id = Guid.NewGuid();

        var result = await _controller.DeleteAsync(id);

        Assert.IsType<NoContentResult>(result);
        await _videoGameService
            .Received(1)
            .DeleteAsync(Arg.Is<VideoGameDeleteCommand>(c => c.Id == id));
    }
}
