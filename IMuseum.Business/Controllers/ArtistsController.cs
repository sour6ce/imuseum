using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Business.Dtos.Artists;
using IMuseum.Business.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Business.Controllers;

//GET /artists
[ApiController]
[Route("artists")]
public class ArtistsController : ControllerBase
{
    private readonly IArtworksRepository artRepository;

    public ArtistsController(IArtworksRepository artRepository)
    {
        this.artRepository = artRepository;
    }

    //GET /artists
    [HttpGet]
    public async Task<ArtistGetReturnDto> GetArtistsAsync()
    {
        IEnumerable<Artwork> artworks = await artRepository.GetObjectsAsync();
        List<string> artists = new List<string>();

        foreach(Artwork art in artworks)
        {
            if(!artists.Contains(art.Author))
                artists.Add(art.Author);
        }

        return new ArtistGetReturnDto()
        {
            Artists = artists.ToArray(),
            Count = artists.Count
        };
    }
}