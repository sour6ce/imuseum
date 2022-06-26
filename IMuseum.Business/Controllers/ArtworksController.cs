using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Business.Dtos.Artworks;
using IMuseum.Business.Dtos.Restorations;
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
    private readonly IRestorationsRepository restRepository;
    private readonly ILogger<ArtworksController> logger;

    public ArtworksController(IArtworksRepository artworks, ISculpturesRepository sculptures,
     IPaintingsRepository paints, IRestorationsRepository restorations, ILogger<ArtworksController> logger)
    {
        this.artRepository = artworks;
        this.sculpturesRepository = sculptures;
        this.paintsRepository = paints;
        this.restRepository = restorations;
        this.logger = logger;
    }

    internal async Task<bool> IsSculpture(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.sculpturesRepository.ContainsAsync(artId);
    }
    internal async Task<bool> IsPainting(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.paintsRepository.ContainsAsync(artId);
    }

    internal async Task<ArtworkType?> ArtType(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return null; //Isn't the Id of an artwork
        // NOTE: Here goes analysis to get a string that identifies the type of the artwork
        return (await IsSculpture(artId)) ? ArtworkType.Sculpture :
                (await IsPainting(artId)) ? ArtworkType.Painting :
                ArtworkType.Other;
    }

    internal async Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art)
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
            Type = ArtType(art.Id).Result.Value
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

    internal Artwork ArtworkFromDto(ArtworkGeneralDto dto)
    {
        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                var sc = new Sculpture()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Assessment = dto.Assessment,
                    Style = dto.Style,
                    Material = dto.Material
                };
                return sc;
            case ArtworkType.Painting:
                var pnt = new Painting()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Assessment = dto.Assessment,
                    Style = dto.Style,
                    Media = dto.Media
                };
                return pnt;
            default:
                var art = new Artwork()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Assessment = dto.Assessment
                };
                return art;
        }
    }

    internal Restoration RestorationFromDto(RestorationReturnDto dto)
    {
        Restoration restoration = new Restoration()
        {
            Artwork = new Artwork() { Id = dto.Artwork.Id },
            StartDate = (DateTime)dto.StartDate,
            EndDate = dto.DueDate,
            Type = dto.RestorationType
        };

        return restoration;
    }

    //GET /artworks
    [HttpGet]
    public async Task<ArtworkGetReturnDto> GetArtworksAsync([FromQuery] ArtworkGetParamDto args)
    {
        var filtered = (DbSet<Artwork> all) =>
        {
            return
            all.Where((x) => args.Author == null || args.Author.Length == 0 || args.Author.Contains(x.Author))
            .Where((x) => args.Statuses == null || args.Statuses.Length == 0 || args.Statuses.Contains(x.CurrentSatus))
            .Where((x) => args.Type == null || args.Type.Length == 0 || args.Type.Contains(ArtType(x.Id).Result.Value))
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

    //POST /artworks
    [HttpPost]
    public async Task<ActionResult<ArtworkGeneralDto>> CreateArtworkAsync(ArtworkGeneralDto artworkDto)
    {
        // TODO: Add update redirecting in case it exist
        switch (artworkDto.Type)
        {
            case ArtworkType.Sculpture:
                var sc = (Sculpture)ArtworkFromDto(artworkDto);
                await sculpturesRepository.AddAsync(sc);
                return CreatedAtAction(nameof(CreateArtworkAsync), new { Id = sc.Id }, sc);
            case ArtworkType.Painting:
                var pnt = (Painting)ArtworkFromDto(artworkDto);
                await paintsRepository.AddAsync(pnt);
                return CreatedAtAction(nameof(CreateArtworkAsync), new { Id = pnt.Id }, pnt);
            default:
                var art = ArtworkFromDto(artworkDto);
                await artRepository.AddAsync(art);
                return CreatedAtAction(nameof(CreateArtworkAsync), new { Id = art.Id }, art);
        }
    }

    //POST /artworks/{id}/start-restoration
    [HttpPost("{id}/start-restoration")]
    public async Task<RestorationReturnDto> StartArtworkRestorationAsync(int artId, RestorationParamDto args)
    {
        Artwork? artwork = await artRepository.GetObjectAsync(artId);
        if (artwork is null)
        {
            return new RestorationReturnDto()
            {
                Artwork = null,
                StartDate = null,
                DueDate = null,
                RestorationStatus = null,
                RestorationType = null
            };
        }
        artwork.CurrentSatus = Artwork.Status.InRestoration;
        DateTime addDate = DateTime.UtcNow;
        await artRepository.UpdateObjectAsync(artwork);
        RestorationReturnDto returnRestoration = new RestorationReturnDto()
        {
            Artwork = new ArtworkIdDto() { Id = artId },
            StartDate = DateTime.UtcNow,
            DueDate = null,
            RestorationStatus = RestorationStatus.Opened,
            RestorationType = args.RestorationType
        };
        await restRepository.AddAsync(RestorationFromDto(returnRestoration));
        return returnRestoration;
    }

    //POST /artworks/{id}/end-restoration
    [HttpPost("{id}/end-restoration")]
    public async Task<RestorationReturnDto> EndArtworkRestorationAsync(int artId, RestorationParamDto args)
    {
        Artwork? artwork = await artRepository.GetObjectAsync(artId);
        if (artwork is null)
        {
            return new RestorationReturnDto()
            {
                Artwork = null,
                StartDate = null,
                DueDate = null,
                RestorationStatus = null,
                RestorationType = null
            };
        }
        artwork.CurrentSatus = Artwork.Status.InRestoration;
        DateTime endDate = DateTime.UtcNow;
        await artRepository.UpdateObjectAsync(artwork);
        RestorationReturnDto returnRestoration = new RestorationReturnDto()
        {
            Artwork = new ArtworkIdDto() { Id = artId },
            StartDate = DateTime.UtcNow,
            DueDate = endDate,
            RestorationStatus = RestorationStatus.Closed,
            RestorationType = args.RestorationType
        };
        await restRepository.UpdateObjectAsync(RestorationFromDto(returnRestoration));
        return returnRestoration;
    }

    //GET /artworks/{id}
    [HttpGet]
    [Route("/artworks/{id}")]
    public async Task<ActionResult<ArtworkGeneralDto>> GetArtworkAsync(int id)
    {
        var ret = await artRepository.GetObjectAsync(id);
        if (ret == null)
        {
            return NotFound();
        }
        else
        {
            return await ArtworkAsDto(ret);
        }
    }

    [HttpDelete]
    [Route("/artworks/{id}")]
    public async Task<ActionResult> DeleteArtwork(int id)
    {
        var success = await artRepository.RemoveAsync(id);
        if (!success)
        {
            return NotFound();
        }
        else
        {
            return new OkResult();
        }
    }

    [HttpPut]
    [Route("/artworks/{id}")]
    public async Task<ActionResult> UpdateArtwork(int id, ArtworkGeneralDto dto)
    {
        var art = ArtworkFromDto(dto); // TODO: Check Status workflow

        var found = await artRepository.GetObjectAsync(id);

        if (found == null)
            return NotFound();
        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                await sculpturesRepository.UpdateObjectAsync(art as Sculpture);
                return AcceptedAtAction(nameof(UpdateArtwork), art as Sculpture);
            case ArtworkType.Painting:
                await paintsRepository.UpdateObjectAsync(art as Painting);
                return AcceptedAtAction(nameof(UpdateArtwork), art as Painting);
            default:
                await artRepository.UpdateObjectAsync(art);
                return AcceptedAtAction(nameof(UpdateArtwork), art);
        }
    }
}