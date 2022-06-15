using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories;
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
    public async Task<IEnumerable<ArtworkDto>> GetArtworksAsync()
    {
        var artworks = (await repository.GetArtworksAsync())
                        .Select(artwork => artwork.AsDto());

        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {artworks.Count()} items");

        return artworks;
    }

    //GET /artworks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ArtworkDto>> GetArtworkAsync(Guid id)
    {
        var artwork = await repository.GetArtworkAsync(id);

        if (artwork is null)
            return NotFound();

        return artwork.AsDto();
    }

    //POST /items
    [HttpPost]
    public async Task<ActionResult<ArtworkDto>> CreateArtworkAsync(CreateArtworkDto artworkDto)
    {
        Artwork artwork = new()
        {
            ArtworkId = Guid.NewGuid(),
            Title = artworkDto.Title,
            Author = artworkDto.Author,
            CreationDate = artworkDto.CreationDate,
            AddDate = artworkDto.AddDate,
            Period = artworkDto.Period,
            Assessment = artworkDto.Assessment
        };

        await repository.CreateArtworkAsync(artwork);
        return CreatedAtAction(nameof(CreateArtworkAsync), new { id = artwork.ArtworkId }, artwork.AsDto());
    }

    //PUT /artworks/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateArtworkAsync(Guid id, UpdateArtworkDto artworkDto)
    {
        var existingArtwork = await repository.GetArtworkAsync(id);

        if (existingArtwork is null)
            return NotFound();

        Artwork updatedArtwork = existingArtwork with
        {
            Title = artworkDto.Title,
            Author = artworkDto.Author,
            CreationDate = artworkDto.CreationDate,
            AddDate = artworkDto.AddDate,
            Period = artworkDto.Period,
            Assessment = artworkDto.Assessment
        };

        await repository.UpdateArtworkAsync(updatedArtwork);

        return NoContent();
    }

    //DELETE /artworks/{id}
    [HttpDelete]
    public async Task<ActionResult> DeleteArtworkAsync(Guid id)
    {
        var existingArtwork = await repository.GetArtworkAsync(id);

        if (existingArtwork is null)
            return NotFound();

        await repository.DeleteArtworkAsync(id);

        return NoContent();
    }
}