using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories.Rooms;
using IMuseum.Business.Dtos.Artworks;
using IMuseum.Business.Dtos.Restorations;
using IMuseum.Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IMuseum.Auth.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IMuseum.Business.Controllers;

//GET /artworks
[CatalogManager]
[ApiController]
[Route("artworks")]
public class ArtworksController : ControllerBase
{
    private readonly IRoomsRepository roomsRepository;
    private IConvertionService convertionService;
    private readonly ISculpturesRepository sculpturesRepository;
    private readonly IPaintingsRepository paintsRepository;
    private readonly IArtworksRepository artRepository;
    private readonly IRestorationsRepository restRepository;
    private readonly ILogger<ArtworksController> logger;

    public ArtworksController(IArtworksRepository artworks, ISculpturesRepository sculptures,
    IRoomsRepository rooms,
    IConvertionService convSer,
     IPaintingsRepository paints, IRestorationsRepository restorations, ILogger<ArtworksController> logger)
    {
        this.artRepository = artworks;
        this.sculpturesRepository = sculptures;
        this.paintsRepository = paints;
        this.restRepository = restorations;
        this.roomsRepository = rooms;
        this.convertionService = convSer;
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

    internal Artwork ArtworkFromDto(ArtworkPutPostDto dto)
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
                    Image = dto.Image,
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
                    Image = dto.Image,
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
                    Image = dto.Image,
                    Assessment = dto.Assessment
                };
                return art;
        }
    }

    //GET /artworks
    [AllowAnonymous]
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
            .Take(args.PageSize).ToArray();
        }));
        return new ArtworkGetReturnDto()
        {
            Artworks = (artworks).Select((x) => this.ArtworkAsDto(x)).ToArray().Select((x) => x.Result).ToArray(),
            Count = (await count)
        };
    }

    //POST /artworks
    [HttpPost]
    public async Task<ActionResult<ArtworkGeneralDto>> CreateArtworkAsync(ArtworkPutPostDto artworkDto)
    {
        switch (artworkDto.Type)
        {
            case ArtworkType.Sculpture:
                var sc = (Sculpture)ArtworkFromDto(artworkDto);
                await sculpturesRepository.AddAsync(sc);
                return CreatedAtAction(nameof(CreateArtworkAsync), null, artworkDto);
            case ArtworkType.Painting:
                var pnt = (Painting)ArtworkFromDto(artworkDto);
                await paintsRepository.AddAsync(pnt);
                return CreatedAtAction(nameof(CreateArtworkAsync), null, artworkDto);
            default:
                var art = ArtworkFromDto(artworkDto);
                await artRepository.AddAsync(art);
                return CreatedAtAction(nameof(CreateArtworkAsync), null, artworkDto);
        }
    }

    //GET /artworks/{id}
    [AllowAnonymous]
    [HttpGet]
    [Route("{id}")]
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
    [Route("{id}")]
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

    //PUT /artwork/{id}
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateArtworkAsync(int id, ArtworkPutPostDto dto)
    {
        var art = ArtworkFromDto(dto);

        var found = artRepository.GetObjectAsync(id);
        var sculpturefound = sculpturesRepository.GetObjectAsync(id);
        var paintfound = paintsRepository.GetObjectAsync(id);

        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                if (await sculpturefound == null)
                    return NotFound();

                // NOTE: Code for persistance of value in case of wrong null
                var sc = art as Sculpture;
                sc.Style = sculpturefound.Result?.Style;
                sc.Material = sculpturefound.Result?.Material;

                await sculpturesRepository.UpdateObjectAsync(sc);
                return AcceptedAtAction(nameof(UpdateArtworkAsync), dto);

            case ArtworkType.Painting:
                if (await paintfound == null)
                    return NotFound();

                // NOTE: Code for persistance of value in case of wrong null
                var pnt = art as Painting;
                pnt.Style = paintfound.Result?.Style;
                pnt.Media = paintfound.Result?.Media;

                await paintsRepository.UpdateObjectAsync(art as Painting);
                return AcceptedAtAction(nameof(UpdateArtworkAsync), dto);
            default:
                if (await found == null)
                    return NotFound();
                await artRepository.UpdateObjectAsync(art);
                return AcceptedAtAction(nameof(UpdateArtworkAsync), dto);
        }
    }

    //POST /artwork/{id}/move
    [HttpPost]
    [Route("{id}/move-to-room")]
    public async Task<ActionResult> MoveRoomAsync(int id, [FromQuery] int RoomId)
    {
        var room = await roomsRepository.GetObjectAsync(RoomId);

        if (room == null)
            return BadRequest();

        return await artRepository.ExecuteOnDbAsync<ActionResult>(async (set, context) =>
        {
            var art = await set.FirstOrDefaultAsync((x) => id == x.Id);

            if (art == null)
                return NotFound();
            else
            {
                if (
                    art.CurrentSatus == Artwork.ArtworkStatus.OnLoan ||
                    art.CurrentSatus == Artwork.ArtworkStatus.InRestoration
                )
                    return BadRequest("An artwork in restoration or loan can't be moved");

                // TODO: Update on added state for external artwork
                art.CurrentSatus = Artwork.ArtworkStatus.OnDisplay;
                art.RoomId = RoomId;
                await context.SaveChangesAsync();
                return new OkResult();
            }

        });
    }


    //POST /artwork/{id}/move
    [HttpPost]
    [Route("{id}/move-to-storage")]
    public async Task<ActionResult> MoveStorageAsync(int id)
    {
        return await artRepository.ExecuteOnDbAsync<ActionResult>(async (set, context) =>
        {
            var art = await set.FirstOrDefaultAsync((x) => id == x.Id);

            if (art == null)
                return NotFound();
            else
            {
                if (
                    art.CurrentSatus == Artwork.ArtworkStatus.OnLoan ||
                    art.CurrentSatus == Artwork.ArtworkStatus.InRestoration
                )
                    return BadRequest("An artwork in restoration or loan can't be moved");

                // TODO: Update on added state for external artwork
                art.CurrentSatus = Artwork.ArtworkStatus.InStorage;
                await context.SaveChangesAsync();
                return new OkResult();
            }

        });
    }
}

[RestauratorChief]
[ApiController]
[Route("artworks")]
public class ArtworkRestorationController : ControllerBase
{
    private readonly IArtworksRepository artRepository;
    private readonly IRestorationsRepository restRepository;
    private readonly ILogger<ArtworksController> logger;


    public ArtworkRestorationController(
        IArtworksRepository artworks,
        IRestorationsRepository restorations,
        ILogger<ArtworksController> logger
        )
    {
        this.artRepository = artworks;
        this.restRepository = restorations;
        this.logger = logger;
    }

    internal Restoration RestorationFromDto(RestorationReturnDto dto)
    {
        Restoration restoration = new Restoration()
        {
            ArtworkId = dto.Artwork.Id,
            StartDate = (DateTime)dto.StartDate,
            EndDate = dto.DueDate,
            Type = dto.RestorationType
        };

        return restoration;
    }


    //POST /artworks/{id}/end-restoration
    [HttpPost("{id}/end-restoration")]
    public async Task<ActionResult<RestorationReturnDto>> EndArtworkRestorationAsync(int id, RestorationParamDto args)
    {
        Artwork? artwork = await artRepository.GetObjectAsync(id);
        if (artwork is null)
            return NotFound();

        var result = restRepository.ExecuteOnDbAsync(async (set, context) =>
        {
            var result = await set.FirstOrDefaultAsync((x) => x.ArtworkId == id);
            if (result == null)
                return false;
            else
            {
                result.EndDate = DateTime.Now;
                await context.SaveChangesAsync();
                return true;
            }
        });

        if (!(await result))
            return new BadRequestResult();


        var resultArt = artRepository.ExecuteOnDbAsync(async (set, context) =>
        {
            var result = await set.FirstOrDefaultAsync((x) => x.Id == id);
            if (result == null)
                return false;
            else
            {
                result.CurrentSatus = Artwork.ArtworkStatus.InStorage;
                await context.SaveChangesAsync();
                return true;
            }
        });

        await resultArt;
        if (await resultArt)
            return new OkResult();
        else
            return new BadRequestResult();
    }


    //POST /artworks/{id}/start-restoration
    [HttpPost("{id}/start-restoration")]
    public async Task<ActionResult<RestorationReturnDto>> StartArtworkRestorationAsync(int id, RestorationParamDto args)
    {
        Artwork? artwork = await artRepository.GetObjectAsync(id);
        if (artwork is null)
        {
            return NotFound();
        }

        var resultArt = artRepository.ExecuteOnDbAsync<ActionResult>(async (set, context) =>
        {
            var result = await set.FirstOrDefaultAsync((x) => x.Id == id);
            if (result == null)
                return NotFound();
            else
            {
                // NOTE: Checking the state of the artwork
                if (result.CurrentSatus == Artwork.ArtworkStatus.InRestoration)
                    return BadRequest();
                if (result.CurrentSatus == Artwork.ArtworkStatus.OnLoan)
                    return BadRequest();
                result.CurrentSatus = Artwork.ArtworkStatus.InRestoration;
                await context.SaveChangesAsync();
                return new OkResult();
            }
        });

        RestorationReturnDto returnRestoration = new RestorationReturnDto()
        {
            Artwork = new SimpleIdDto() { Id = id },
            StartDate = DateTime.UtcNow,
            DueDate = null,
            RestorationType = args.RestorationType
        };
        await restRepository.AddAsync(RestorationFromDto(returnRestoration));
        return (resultArt.Result.GetType() == typeof(OkResult)) ? returnRestoration : resultArt.Result;
    }
}