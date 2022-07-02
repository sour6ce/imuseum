
using IMuseum.Auth.Authorization;
using Microsoft.AspNetCore.Mvc;
using IMuseum.Business.Dtos;

namespace IMuseum.Business.Controllers;

//GET /statics
[Authorize]
[ApiController]
[Route("statics")]
public class StaticController : ControllerBase
{
    public StaticController()
    {
    }

    //GET /statics/artwork-types
    [HttpGet]
    [Route("artwork-types")]
    public SimpleDto[] GetArtworkTypes()
    {
        return Utils.ArtworkTypeNameMaps().Item2.Select(x => new SimpleDto { Id = (int)x.Key, Name = x.Value }).OrderBy(x => x.Id).ToArray();
    }

    //GET /statics/artwork-statuses
    [HttpGet]
    [Route("artwork-statuses")]
    public SimpleDto[] GetArtworkStatuses()
    {
        return Utils.ArtworkStatusNameMaps().Item2.Select(x => new SimpleDto { Id = (int)x.Key, Name = x.Value }).OrderBy(x => x.Id).ToArray();
    }

    //GET /statics/loan-applications-statuses
    [HttpGet]
    [Route("loan-applications-statuses")]
    public SimpleDto[] GetLoanAppStatuses()
    {
        return Utils.LoanAppStatusNameMap().Item2.Select(x => new SimpleDto { Id = (int)x.Key, Name = x.Value }).OrderBy(x => x.Id).ToArray();
    }

    //GET /statics/restoration-types
    [HttpGet]
    [Route("restoration-types")]
    public SimpleDto[] GetRestorationTypes()
    {
        return Utils.RestorationTypeNameMap().Item2.Select(x => new SimpleDto { Id = (int)x.Key, Name = x.Value }).OrderBy(x => x.Id).ToArray();
    }
}