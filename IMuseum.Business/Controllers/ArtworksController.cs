using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Business.Dtos.Artworks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Business.Controllers;

//GET /artworks
[ApiController]
[Route("artworks")]
public class ArtworksController : ControllerBase
{
    private readonly ISculpturesRepository sculpturesRepository;
    private readonly IPaintingsRepository paintsRepository;
    private readonly IArtworksRepository artRepository;
    private readonly ILogger<ArtworksController> logger;

    public ArtworksController(IArtworksRepository artworks, ISculpturesRepository sculptures,
     IPaintingsRepository paints, ILogger<ArtworksController> logger)
    {
        this.artRepository = artworks;
        this.sculpturesRepository = sculptures;
        this.paintsRepository = paints;
        this.logger = logger;
    }

    public async Task<bool> IsSculpture(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.sculpturesRepository.ContainsAsync(artId);
    }
    public async Task<bool> IsPainting(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.paintsRepository.ContainsAsync(artId);
    }

    public async Task<ArtworkType?> ArtType(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return null; //Isn't the Id of an artwork
        // NOTE: Here goes analysis to get a string that identifies the type of the artwork
        return (await IsSculpture(artId)) ? ArtworkType.Sculpture :
                (await IsPainting(artId)) ? ArtworkType.Painting :
                ArtworkType.Other;
    }

    public async Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art)
    {
        var sc = sculpturesRepository.GetObjectAsync(art.Id);
        var pnt = paintsRepository.GetObjectAsync(art.Id);

        var dto = new ArtworkGeneralDto()
        {
            Id = art.Id.GetHashCode(),
            Title = art.Title,
            Description = art.Description,
            Author = art.Author,
            CreationDate = art.CreationDate,
            IncorporatedDate = art.IncorporatedDate,
            Period = art.Period,
            Assessment = art.Assessment,
            Status = art.CurrentSatus,
            Type = ArtType(art.Id).Result
        };

        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                var tempsc = await sc;
                dto.Style = tempsc?.Style;
                dto.Material = tempsc?.Material;
                break;
            case ArtworkType.Painting:
                var temppnt = await pnt;
                dto.Style = temppnt?.Style;
                dto.Media = temppnt?.Media;
                break;
            default:
                break;
        }
        return dto;
    }

    //GET /artworks
    [HttpGet]
    public async Task<ArtworkGetReturnDto> GetArtworksAsync(ArtworkGetParamDto args)
    {
        var filtered = (DbSet<Artwork> all) =>
        {
            return
            all.Where((x) => args.Author.Length == 0 || args.Author.Contains(x.Author))
            .Where((x) => args.Statuses.Length == 0 || args.Statuses.Contains(x.CurrentSatus))
            .Where((x) => args.Type.Length == 0 || args.Type.Contains(ArtType(x.Id).Result.Value))
            .Where((x) => args.Search == null || args.Search == "" || x.Title.Contains(args.Search));
        };
        var count = (artRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var artworks = (artRepository.ExecuteOnDb((all) =>
        {
            return
            filtered(all).Skip(args.PageSize * (args.Page - 1))
            .Take(args.PageSize);
        }));
        return new ArtworkGetReturnDto()
        {
            Artworks = (artworks).Select((x) => this.ArtworkAsDto(x).Result).ToArray(),
            Count = (await count)
        };
    }
}