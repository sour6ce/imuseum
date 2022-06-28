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
    private readonly IConvertionService convertionService;
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
        this.convertionService = new ConvertionService(artworks, sculptures, paints);
        this.artRepository = artworks;
        this.sculpturesRepository = sculptures;
        this.paintsRepository = paints;
        this.restRepository = restorations;
        this.roomsRepository = rooms;
        this.convertionService = convSer;
        this.logger = logger;
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
            .Where((x) => args.Statuses == null || args.Statuses.Length == 0 || args.Statuses.Contains(Utils.ArtworkStatusNameMaps().Item2[x.CurrentStatus]))
            .Where((x) => args.Type == null || args.Type.Length == 0 || args.Type.Contains((Utils.ArtworkTypeNameMaps().Item2[convertionService.ArtType(x.Id).Result.Value])))
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
            Artworks = (artworks).Select((x) => this.convertionService.ArtworkAsDto(x)).ToArray().Select((x) => x.Result).ToArray(),
            Count = (await count)
        };
    }

    //POST /artworks
    [HttpPost]
    public async Task<ActionResult<ArtworkGeneralDto>> CreateArtworkAsync(ArtworkPutPostDto artworkDto)
    {
        ArtworkType type;

        try
        {
            type = (ArtworkType)(int.Parse(artworkDto.Type));
        }
        catch
        {
            type = Utils.ArtworkTypeNameMaps().Item1[artworkDto.Type];
        }

        switch (type)
        {
            case ArtworkType.Sculpture:
                var sc = (Sculpture)convertionService.ArtworkFromDto(artworkDto);
                await sculpturesRepository.AddAsync(sc);
                return CreatedAtAction(nameof(CreateArtworkAsync), null, artworkDto);
            case ArtworkType.Painting:
                var pnt = (Painting)convertionService.ArtworkFromDto(artworkDto);
                await paintsRepository.AddAsync(pnt);
                return CreatedAtAction(nameof(CreateArtworkAsync), null, artworkDto);
            default:
                var art = convertionService.ArtworkFromDto(artworkDto);
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
            return await convertionService.ArtworkAsDto(ret);
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
        var art = convertionService.ArtworkFromDto(dto);

        var found = artRepository.GetObjectAsync(id);
        var sculpturefound = sculpturesRepository.GetObjectAsync(id);
        var paintfound = paintsRepository.GetObjectAsync(id);

        ArtworkType type;

        try
        {
            type = (ArtworkType)(int.Parse(dto.Type));
        }
        catch
        {
            type = Utils.ArtworkTypeNameMaps().Item1[dto.Type];
        }

        switch (type)
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
                    art.CurrentStatus == Artwork.ArtworkStatus.OnLoan ||
                    art.CurrentStatus == Artwork.ArtworkStatus.InRestoration
                )
                    return BadRequest("An artwork in restoration or loan can't be moved");

                // TODO: Update on added state for external artwork
                art.CurrentStatus = Artwork.ArtworkStatus.OnDisplay;
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
                    art.CurrentStatus == Artwork.ArtworkStatus.OnLoan ||
                    art.CurrentStatus == Artwork.ArtworkStatus.InRestoration
                )
                    return BadRequest("An artwork in restoration or loan can't be moved");

                // TODO: Update on added state for external artwork
                art.CurrentStatus = Artwork.ArtworkStatus.InStorage;
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
    private readonly IConvertionService convertionService;
    private readonly ILogger<ArtworksController> logger;


    public ArtworkRestorationController(
        IArtworksRepository artworks,
        IRestorationsRepository restorations,
        IConvertionService conVer,
        ILogger<ArtworksController> logger
        )
    {
        this.artRepository = artworks;
        this.restRepository = restorations;
        this.convertionService = conVer;
        this.logger = logger;
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
                result.CurrentStatus = Artwork.ArtworkStatus.InStorage;
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
                if (result.CurrentStatus == Artwork.ArtworkStatus.InRestoration)
                    return BadRequest();
                if (result.CurrentStatus == Artwork.ArtworkStatus.OnLoan)
                    return BadRequest();
                result.CurrentStatus = Artwork.ArtworkStatus.InRestoration;
                await context.SaveChangesAsync();
                return new OkResult();
            }
        });

        RestorationReturnDto returnRestoration = new RestorationReturnDto()
        {
            Artwork = await convertionService.ArtworkAsDto(artwork),
            StartDate = DateTime.UtcNow,
            DueDate = null,
            RestorationType = args.RestorationType
        };
        await restRepository.AddAsync(convertionService.RestorationFromDto(returnRestoration));
        return (resultArt.Result.GetType() == typeof(OkResult)) ? returnRestoration : resultArt.Result;
    }
}