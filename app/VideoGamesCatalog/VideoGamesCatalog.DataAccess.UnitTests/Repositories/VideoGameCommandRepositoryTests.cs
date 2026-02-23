using Microsoft.EntityFrameworkCore;
using NSubstitute;
using VideoGamesCatalog.Core.Models;
using VideoGamesCatalog.Core.Specification;
using VideoGamesCatalog.DataAccess.EntityModels;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories;

namespace VideoGamesCatalog.DataAccess.UnitTests.Repositories;

public sealed class VideoGameCommandRepositoryTests
{
    private readonly IVideoGamesCatalogDbContext _dbContextStub = Substitute.For<IVideoGamesCatalogDbContext>();
    private readonly DbSet<VideoGameEntity> _videoGamesDbSetStub = Substitute.For<DbSet<VideoGameEntity>>();
    private readonly VideoGameCommandRepository _sut;

    public VideoGameCommandRepositoryTests()
    {
        _dbContextStub.VideoGames.Returns(_videoGamesDbSetStub);
        _sut = new VideoGameCommandRepository(_dbContextStub);
    }

    [Fact]
    public async Task AddAsync_WhenCalled_AddsEntityToDbSet()
    {
        var specification = new VideoGameAddSpecification("Halo", "Shooter");

        await _sut.AddAsync(specification);

        _videoGamesDbSetStub.Received(1).Add(Arg.Is<VideoGameEntity>(e =>
            e.Title == "Halo" && e.Description == "Shooter"));
    }

    [Fact]
    public async Task AddAsync_WhenCalled_CallsSaveChangesAsync()
    {
        var specification = new VideoGameAddSpecification("Halo", "Shooter");

        await _sut.AddAsync(specification);

        await _dbContextStub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AddAsync_WhenCalled_ReturnsEntityId()
    {
        var expectedId = Guid.NewGuid();
        _videoGamesDbSetStub.When(x => x.Add(Arg.Any<VideoGameEntity>()))
            .Do(callInfo => callInfo.Arg<VideoGameEntity>().Id = expectedId);
        var specification = new VideoGameAddSpecification("Halo", "Shooter");

        var result = await _sut.AddAsync(specification);

        Assert.Equal(expectedId, result);
    }

    [Fact]
    public async Task AddAsync_WhenDescriptionIsNull_AddsEntityWithNullDescription()
    {
        var specification = new VideoGameAddSpecification("Zelda", null);

        await _sut.AddAsync(specification);

        _videoGamesDbSetStub.Received(1).Add(Arg.Is<VideoGameEntity>(e =>
            e.Title == "Zelda" && e.Description == null));
    }

    [Fact]
    public async Task UpdateAsync_WhenCalled_UpdatesEntityInDbSet()
    {
        var id = Guid.NewGuid();
        byte[] rowVersion = [1, 2, 3];
        var domain = new VideoGameDomain(id, "Halo Updated", "FPS", rowVersion);

        await _sut.UpdateAsync(domain);

        _videoGamesDbSetStub.Received(1).Update(Arg.Is<VideoGameEntity>(e =>
            e.Id == id &&
            e.Title == "Halo Updated" &&
            e.Description == "FPS" &&
            e.RowVersion == rowVersion));
    }

    [Fact]
    public async Task UpdateAsync_WhenCalled_CallsSaveChangesAsync()
    {
        var domain = new VideoGameDomain(Guid.NewGuid(), "Halo", "Shooter", [1]);

        await _sut.UpdateAsync(domain);

        await _dbContextStub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_WhenDescriptionIsNull_UpdatesEntityWithNullDescription()
    {
        var id = Guid.NewGuid();
        var domain = new VideoGameDomain(id, "Zelda", null, [5]);

        await _sut.UpdateAsync(domain);

        _videoGamesDbSetStub.Received(1).Update(Arg.Is<VideoGameEntity>(e =>
            e.Id == id &&
            e.Title == "Zelda" &&
            e.Description == null));
    }
}
