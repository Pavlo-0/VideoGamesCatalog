using MockQueryable.NSubstitute;
using NSubstitute;
using VideoGamesCatalog.DataAccess.EntityModels;
using VideoGamesCatalog.DataAccess.Persistence;
using VideoGamesCatalog.DataAccess.Repositories;

namespace VideoGamesCatalog.DataAccess.UnitTests.Repositories;

public sealed class VideoGameQueryRepositoryTests
{
    private readonly IVideoGamesCatalogDbContext _dbContextStub = Substitute.For<IVideoGamesCatalogDbContext>();
    private readonly VideoGameQueryRepository _sut;

    public VideoGameQueryRepositoryTests()
    {
        _sut = new VideoGameQueryRepository(_dbContextStub);
    }

    [Fact]
    public async Task GetByIdAsync_WhenEntityExists_ReturnsMappedDomain()
    {
        var id = Guid.NewGuid();
        SetupVideoGames(new VideoGameEntity { Id = id, Title = "Halo", Description = "Shooter", RowVersion = [1] });

        var result = await _sut.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Halo", result.Title);
        Assert.Equal("Shooter", result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_WhenEntityDoesNotExist_ReturnsNull()
    {
        SetupVideoGames();

        var result = await _sut.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenEntitiesExist_ReturnsAllMappedDomains()
    {
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        SetupVideoGames(
            new VideoGameEntity { Id = firstId, Title = "Halo", Description = "Shooter", RowVersion = [1] },
            new VideoGameEntity { Id = secondId, Title = "Zelda", Description = null, RowVersion = [2] });

        var result = (await _sut.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, g => g.Id == firstId && g.Title == "Halo");
        Assert.Contains(result, g => g.Id == secondId && g.Title == "Zelda");
    }

    [Fact]
    public async Task GetAllAsync_WhenNoEntitiesExist_ReturnsEmptyCollection()
    {
        SetupVideoGames();

        var result = await _sut.GetAllAsync();

        Assert.Empty(result);
    }

    private void SetupVideoGames(params VideoGameEntity[] entities)
    {
        var dbSet = entities.BuildMockDbSet();

        _dbContextStub.VideoGames.Returns(dbSet);
    }
}
