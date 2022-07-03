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
using IMuseum.Auth.Authorization;
namespace IMuseum.Business.Controllers;

//GET /artists
[CatalogManager]
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
    [AllowAnonymous]
    [HttpGet]
    public async Task<ArtistGetReturnDto> GetArtistsAsync([FromQuery] string? search = "")
    {
        search = search ?? "";

        var filter = (DbSet<Artwork> x) => x.Where(y => y.Author.ToLower().Contains(search.ToLower()));

        var artists = artRepository.ExecuteOnDbAsync(
            async x => await filter(x).Select(y => y.Author).Distinct().ToArrayAsync()
        );

        var count = artRepository.ExecuteOnDbAsync(
            async x => await filter(x).CountAsync()
        );

        return new ArtistGetReturnDto()
        {
            Artists = await artists,
            Count = await count
        };
    }
}