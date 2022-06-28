using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Business.Dtos.Restorations;
using IMuseum.Business.Dtos.Artworks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;
using IMuseum.Business.Dtos;

namespace IMuseum.Business.Controllers;

[RestauratorChief]
[ApiController]
[Route("restorations")]
public class RestorationsController : ControllerBase
{
    private readonly IConvertionService convertionService;
    private readonly IRestorationsRepository restRepository;

    public RestorationsController(IArtworksRepository artworks, ISculpturesRepository sculptures,
    IConvertionService convSer,
     IPaintingsRepository paints, IRestorationsRepository restRepository)
    {
        this.convertionService = convSer;
        this.restRepository = restRepository;
    }

    internal async Task<RestorationReturnDto> RestorationAsDto(Restoration restoration)
    {
        var dto = new RestorationReturnDto()
        {
            Artwork = await convertionService.ArtworkAsDto(restoration.Artwork),
            StartDate = restoration.StartDate,
            DueDate = restoration.EndDate,
            RestorationType = Utils.RestorationTypeNameMap().Item2[restoration.Type],
        };
        return dto;
    }

    //GET /restorations
    [HttpGet]
    public async Task<RestorationGetReturnDto> GetRestorationsAsync([FromQuery] RestorationGetParamDto args)
    {
        var filtered = (DbSet<Restoration> all) =>
        {
            return
            all.Where((x) => args.ArtworksIds == null || args.ArtworksIds.Length == 0 || args.ArtworksIds.Contains(x.Artwork.Id))
            .Where((x) =>
                (args.StartDateA == null && args.StartDateB == null) ||
                (args.StartDateA != null && x.StartDate >= args.StartDateA && args.StartDateB == null) ||
                (args.StartDateB != null && x.StartDate == null && x.StartDate <= args.StartDateB) ||
                (x.StartDate >= args.StartDateA && x.StartDate <= args.StartDateB))
            .Where((x) =>
                (args.EndDateA == null && args.EndDateB == null) ||
                (args.EndDateA != null && x.EndDate >= args.EndDateA && args.EndDateB == null) ||
                (args.EndDateB != null && x.EndDate == null && x.EndDate <= args.EndDateB) ||
                (x.EndDate >= args.EndDateA && x.EndDate <= args.EndDateB));
        };
        var count = (restRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var restorations = (restRepository.ExecuteOnDb((all) =>
        {
            return
            filtered(all).Skip(args.PageSize * (args.Page - 1))
            .Take(args.PageSize).ToArray();
        }));
        return new RestorationGetReturnDto()
        {
            Restorations = (restorations).Select((x) => this.RestorationAsDto(x)).ToArray().Select((x) => x.Result).ToArray(),
            Count = (await count)
        };
    }
}