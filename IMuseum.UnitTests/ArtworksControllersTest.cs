namespace IMuseum.UnitTests;

public class ArtworksControllerTests
{
    private readonly Mock<IArtworksRepository> artsStub = new();
    private readonly Mock<ISculpturesRepository> scsStub = new();
    private readonly Mock<IRoomsRepository> roomsStub = new();
    private readonly Mock<IPaintingsRepository> paintsStub = new();
    private readonly Mock<IRestorationsRepository> restsStub = new();
    private readonly Mock<IConvertionService> convertionService = new();

    private readonly Mock<ILogger<ArtworksController>> loggerStub = new();
    private readonly Random rand = new();

    [Fact]
    public async Task GetArtworkAsync_WithUnexistingArtwork_RetursnNotFound()
    {
        //Arrange
        var repositoryStub = new Mock<IArtworksRepository>();
        repositoryStub.Setup(repo => repo.GetObjectAsync(It.IsAny<int>()))
            .ReturnsAsync((Artwork)null);

        var loggerStub = new Mock<ILogger<ArtworksController>>();

        var controller = new ArtworksController(artsStub.Object, scsStub.Object, roomsStub.Object, convertionService.Object, paintsStub.Object, restsStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetArtworkAsync(rand.Next(500, 5000000));

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task CreateArtworkAsync_WithArtworkToCreate_ReturnsCreatedArtwork()
    {
        //Arrange
        var artworkToCreate = new ArtworkPutPostDto()
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            CreationDate = DateTime.Now,
            IncorporatedDate = DateTime.Now,
            Period = Guid.NewGuid().ToString(),
            Assessment = rand.Next(50000000)
        };

        var controller = new ArtworksController(artsStub.Object, scsStub.Object, roomsStub.Object, convertionService.Object, paintsStub.Object, restsStub.Object, loggerStub.Object);

        //Act
        var result = await controller.CreateArtworkAsync(artworkToCreate);

        //Assert
        var createdArtwork = (result.Result as CreatedAtActionResult).Value as ArtworkPutPostDto;
        artworkToCreate.Should().BeEquivalentTo(
            createdArtwork,
            options => options.ComparingByMembers<ArtworkPutPostDto>().ExcludingMissingMembers()
        );
    }

    [Fact]
    public async Task UpdateArtworkAsync_WithExistingArtwork_ReturnsNoContent()
    {
        //Arrange
        Artwork existingArtwork = CreateRandomArtwork();
        artsStub.Setup(repo => repo.GetObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(existingArtwork);

        var artworkId = existingArtwork.Id;
        var artworkToUpdate = new ArtworkPutPostDto()
        {
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            CreationDate = DateTime.Now,
            IncorporatedDate = DateTime.Now,
            Period = Guid.NewGuid().ToString(),
            Assessment = rand.Next(50000000)
        };

        var controller = new ArtworksController(artsStub.Object, scsStub.Object, roomsStub.Object, convertionService.Object, paintsStub.Object, restsStub.Object, loggerStub.Object);

        //Act
        var result = await controller.UpdateArtworkAsync(artworkId, artworkToUpdate);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteArtworkAsync_WithExistingArtwork_ReturnsNoContent()
    {
        //Arrange
        Artwork existingArtwork = CreateRandomArtwork();
        artsStub.Setup(repo => repo.GetObjectAsync(It.IsAny<int>()))
            .ReturnsAsync(existingArtwork);

        var controller = new ArtworksController(artsStub.Object, scsStub.Object, roomsStub.Object, convertionService.Object, paintsStub.Object, restsStub.Object, loggerStub.Object);

        //Act
        var result = await controller.DeleteArtwork(existingArtwork.Id);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    private Artwork CreateRandomArtwork()
    {
        return new()
        {
            Id = rand.Next(500, 5000000),
            Title = Guid.NewGuid().ToString(),
            Author = Guid.NewGuid().ToString(),
            CreationDate = DateTime.Now,
            IncorporatedDate = DateTime.Now,
            Period = Guid.NewGuid().ToString(),
            Assessment = rand.Next(500, 50000000)
        };
    }
}