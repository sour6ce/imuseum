using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Business.Dtos;
using IMuseum.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IMuseum.Business.Controllers;

//GET /artworks
[ApiController]
[Route("artworks")]
public class ArtworksController : ControllerBase
{
    private readonly IArtworksRepository repository;
    private readonly ILogger<ArtworksController> logger;

    public ArtworksController(IArtworksRepository repository, ILogger<ArtworksController> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    //GET /artworks
    [HttpGet]
    public async Task<IEnumerable<InternalArtworkDto>> GetArtworksAsync()
    {
        var artworks = (await repository.GetObjectsAsync())
                        .Select(artwork => artwork.AsDto());

        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {artworks.Count()} items");

        return artworks;
    }

    //GET /artworks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<InternalArtworkDto>> GetArtworkAsync(Guid id)
    {
        var artwork = await repository.GetObjectAsync(id);

        if (artwork is null)
            return NotFound();

        return artwork.AsDto();
    }

    //POST /items
    [HttpPost]
    public async Task<ActionResult<InternalArtworkDto>> CreateArtworkAsync(CreateInternalArtworkDto InternalArtworkDto)
    {
        Artwork artwork = new()
        {
            Id = Guid.NewGuid(),
            Title = InternalArtworkDto.Title,
            Author = InternalArtworkDto.Author,
            CreationDate = InternalArtworkDto.CreationDate,
            IncorporatedDate = InternalArtworkDto.AddDate,
            Period = InternalArtworkDto.Period,
            Assessment = InternalArtworkDto.Assessment
        };

        await repository.AddAsync(artwork);

        return CreatedAtAction(nameof(CreateArtworkAsync), new { id = artwork.Id }, artwork.AsDto());
    }

    //PUT /artworks/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateArtworkAsync(Guid id, UpdateInternalArtworkDto InternalArtworkDto)
    {
        var existingArtwork = await repository.GetObjectAsync(id);

        if (existingArtwork is null)
            return NotFound();

        Artwork updatedArtwork = existingArtwork with
        {
            Title = InternalArtworkDto.Title,
            Author = InternalArtworkDto.Author,
            CreationDate = InternalArtworkDto.CreationDate,
            IncorporatedDate = InternalArtworkDto.AddDate,
            Period = InternalArtworkDto.Period,
            Assessment = InternalArtworkDto.Assessment
        };

        await repository.UpdateObjectAsync(updatedArtwork);

        return NoContent();
    }

    //DELETE /artworks/{id}
    [HttpDelete]
    public async Task<ActionResult> DeleteArtworkAsync(Guid id)
    {
        var existingArtwork = await repository.GetObjectAsync(id);

        if (existingArtwork is null)
            return NotFound();

        await repository.RemoveAsync(id);

        return NoContent();
    }
}