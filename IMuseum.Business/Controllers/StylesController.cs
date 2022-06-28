using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.PlasticArts;
using IMuseum.Business.Dtos.Styles;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Business.Dtos.Artists;
using IMuseum.Business.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;

namespace IMuseum.Business.Controllers;

//GET /styles
[CatalogManager]
[ApiController]
[Route("styles")]
public class StylesController : ControllerBase
{
    private readonly IPlasticArtsRepository artRepository;

    public StylesController(IPlasticArtsRepository artRepository)
    {
        this.artRepository = artRepository;
    }

    //GET /artists
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<StyleGetReturnDto> GetArtistsAsync()
    {
        IEnumerable<PlasticArt> artworks = await artRepository.GetObjectsAsync();
        List<string> styles = new List<string>();

        foreach(PlasticArt art in artworks)
        {
            if(!styles.Contains(art.Style))
                styles.Add(art.Style);
        }

        return new StyleGetReturnDto()
        {
            Styles = styles.ToArray(),
            Count = styles.Count
        };
    }
}