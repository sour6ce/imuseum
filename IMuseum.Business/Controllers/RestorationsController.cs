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

namespace IMuseum.Business.Controllers;

[ApiController]
[Route("restorations")]
public class RestorationsController : ControllerBase
{
    private readonly IRestorationsRepository restRepository;

    public RestorationsController(IRestorationsRepository restRepository)
    {
        this.restRepository = restRepository;
    }

    internal async Task<RestorationReturnDto> RestorationAsDto(Restoration restoration)
    {
        var dto = new RestorationReturnDto()
        {
            StartDate = restoration.StartDate,
            DueDate = restoration.EndDate,
            RestorationType = restoration.Type,
            RestorationStatus = restoration.EndDate == null ? RestorationStatus.Opened : RestorationStatus.Closed
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
            .Where((x) => args.Statuses == null || args.Statuses.Length == 0 || args.Statuses.Contains(RestorationStatus.Opened) || args.Statuses.Contains(RestorationStatus.Closed))
            .Where((x) => x.StartDate >= args.StartDateA && x.StartDate <= args.StartDateB)
            .Where((x) => (x.EndDate == null) || (x.EndDate >= args.EndDateA && x.EndDate <= args.EndDateB));
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
            .Take(args.PageSize);
        }));
        return new RestorationGetReturnDto()
        {
            Restorations = (restorations).Select((x) => this.RestorationAsDto(x).Result).ToArray(),
            Count = (await count)
        };
    }
}