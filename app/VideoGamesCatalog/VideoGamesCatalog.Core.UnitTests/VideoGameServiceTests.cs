using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using VideoGamesCatalog.Core.Commands.VideoGame;
using VideoGamesCatalog.Core.DataAccessInterfaces;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Services;
using VideoGamesCatalog.Core.Specification;

namespace VideoGamesCatalog.Core.UnitTests.Services;

public sealed class VideoGameServiceTests
{
    private readonly IVideoGameCommandRepository _commandRepository = Substitute.For<IVideoGameCommandRepository>();
    private readonly IVideoGameQueryRepository _queryRepository = Substitute.For<IVideoGameQueryRepository>();
    private readonly VideoGameService _service;

    public VideoGameServiceTests()
    {
        _service = new VideoGameService(_commandRepository, _queryRepository, NullLogger<VideoGameService>.Instance);
    }

    [Fact]
    public async Task AddAsync_ReturnsNewIdentifierAndUsesSpecification()
    {
        var expectedId = Guid.NewGuid();
        var command = new VideoGameAddCommand("Halo", "Shooter");
        _commandRepository
            .AddAsync(Arg.Any<VideoGameAddSpecification>())
            .Returns(expectedId);

        var result = await _service.AddAsync(command);

        Assert.Equal(expectedId, result);
        await _commandRepository
            .Received(1)
            .AddAsync(Arg.Is<VideoGameAddSpecification>(s =>
                s.Title == command.Title && s.Description == command.Description));
    }

    [Fact]
    public async Task UpdateAsync_WhenGameIsMissing_ReturnsFalseAndSkipsUpdate()
    {
        var command = new VideoGameUpdateCommand(Guid.NewGuid(), "New", "Desc", [1]);
        _queryRepository
            .GetByIdAsync(command.Id)
            .Returns((VideoGameDomain?)null);

        var result = await _service.UpdateAsync(command);

        Assert.False(result);
        await _commandRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<VideoGameDomain>());
    }

    [Fact]
    public async Task UpdateAsync_WhenGameExists_UpdatesDomainAndPersists()
    {
        var id = Guid.NewGuid();
        var existing = new VideoGameDomain(id, "Old", "OldDesc", [0]);
        var command = new VideoGameUpdateCommand(id, "Updated", "UpdatedDesc", [5]);
        _queryRepository
            .GetByIdAsync(id)
            .Returns(existing);

        var result = await _service.UpdateAsync(command);

        Assert.True(result);
        await _commandRepository
            .Received(1)
            .UpdateAsync(existing);
        Assert.Equal("Updated", existing.Title);
        Assert.Equal("UpdatedDesc", existing.Description);
        Assert.Equal([5], existing.RowVersion);
    }

    [Fact]
    public async Task DeleteAsync_RemovesGameFromRepository()
    {
        var expectedId = Guid.NewGuid();

        await _service.DeleteAsync(new VideoGameDeleteCommand(expectedId));

        await _commandRepository
            .Received(1)
            .RemoveAsync(expectedId);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsDataAndPreservesCancellationToken()
    {
        var expected = new[] { new VideoGameDomain(Guid.NewGuid(), "Title", "Description", [1]) };
        using var cancellation = new CancellationTokenSource();
        _queryRepository
            .GetAllAsync(cancellation.Token)
            .Returns(expected);

        var result = await _service.GetAllAsync(cancellation.Token);

        Assert.Equal(expected.Count(), result.Count());
        Assert.Equal(expected[0].Title, result.First().Title);
        Assert.Equal(expected[0].Description, result.First().Description);
        await _queryRepository
            .Received(1)
            .GetAllAsync(cancellation.Token);
    }

}
